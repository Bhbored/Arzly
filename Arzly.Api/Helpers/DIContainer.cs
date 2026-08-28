using Arzly.Api.Application.Contracts.Auth;
using Arzly.Api.Application.Contracts.Admin;
using Arzly.Api.Application.Contracts.Categories;
using Arzly.Api.Application.Contracts.Communications;
using Arzly.Api.Application.Contracts.Listings;
using Arzly.Api.Application.Contracts.Locations;
using Arzly.Api.Application.Contracts.Support;
using Arzly.Api.Application.Contracts.Users;
using Arzly.Api.Application.Services.Auth;
using Arzly.Api.Application.Services.Admin;
using Arzly.Api.Application.Services.Categories;
using Arzly.Api.Application.Services.Communications;
using Arzly.Api.Application.Services.Listings;
using Arzly.Api.Application.Services.Locations;
using Arzly.Api.Application.Services.Support;
using Arzly.Api.Application.Services.Users;
using Arzly.Api.Domain.Contracts.Categories;
using Arzly.Api.Domain.Contracts.Communications;
using Arzly.Api.Domain.Contracts.Listings;
using Arzly.Api.Domain.Contracts.Locations;
using Arzly.Api.Domain.Contracts.Support;
using Arzly.Api.Domain.Contracts.Users;
using Arzly.Api.Filters.HubFilters;
using Arzly.Api.Filters.ResultFilters;
using Arzly.Api.Helpers.GoogleMap;
using Arzly.Api.Hubs.Contracts;
using Arzly.Api.Hubs.Services;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Api.Infrastructure.Identity;
using Arzly.Api.Infrastructure.Health;
using Arzly.Api.Infrastructure.HostedServices;
using Arzly.Api.Infrastructure.Repositories.Categories;
using Arzly.Api.Infrastructure.Repositories.Communications;
using Arzly.Api.Infrastructure.Repositories.Listings;
using Arzly.Api.Infrastructure.Repositories.Locations;
using Arzly.Api.Infrastructure.Repositories.Support;
using Arzly.Api.Infrastructure.Repositories.Users;
using Arzly.Api.Infrastructure.Storage;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security.Claims;
using System.Threading.RateLimiting;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arzly.Api.Helpers
{
    public static class DIContainer
    {

        public static IServiceCollection RegisterGoogleAuthClient(this IServiceCollection services, IConfiguration configuration, IHostEnvironment? environment = null)
        {
            if (environment?.IsEnvironment("Test") != true)
            {
                services.AddAuthentication().AddGoogleOpenIdConnect(googleOptions =>
                {
                    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
                });
            }
            return services;
        }
        public static IServiceCollection RegisterApiVersioning(this IServiceCollection services)
        {
            var apiVersioningBuilder = services.AddApiVersioning(config =>
            {
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
            });
            apiVersioningBuilder.AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            return services;
        }

        public static IServiceCollection RegisterCors(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment? environment = null)
        {
            var origins = configuration.GetSection("AllowedOrigins").Get<string[]>() ?? [];
            origins = origins.Where(origin =>
            {
                if (!Uri.TryCreate(origin, UriKind.Absolute, out var uri) ||
                    (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
                    throw new InvalidOperationException($"Invalid CORS origin: {origin}");
                return true;
            }).Distinct(StringComparer.OrdinalIgnoreCase).ToArray();
            if (environment?.IsProduction() == true && origins.Length == 0)
                throw new InvalidOperationException("AllowedOrigins must contain at least one production client origin");

            services.AddCors(options =>
            {
                options.AddPolicy("ArzlyClients", policy =>
                {
                    if (origins.Length > 0)
                    {
                        policy.WithOrigins(origins);
                        policy.AllowCredentials();
                    }
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
            });
            return services;
        }

        public static IServiceCollection RegisterHealthAndRateLimiting(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddCheck<DatabaseHealthCheck>("database", tags: ["ready"])
                .AddCheck<ExternalServicesConfigurationHealthCheck>(
                    "external-configuration",
                    tags: ["ready", "dependencies"]);

            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.OnRejected = (context, _) =>
                {
                    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                    {
                        context.HttpContext.Response.Headers.RetryAfter =
                            Math.Max(1, (int)Math.Ceiling(retryAfter.TotalSeconds)).ToString();
                    }

                    return ValueTask.CompletedTask;
                };
                AddFixedWindowPolicy(options, configuration, "auth", "Auth", 10, TimeSpan.FromMinutes(1));
                AddFixedWindowPolicy(options, configuration, "uploads", "Uploads", 20, TimeSpan.FromMinutes(1));
                AddFixedWindowPolicy(options, configuration, "messaging", "Messaging", 60, TimeSpan.FromMinutes(1));
                AddFixedWindowPolicy(options, configuration, "reports", "Reports", 10, TimeSpan.FromMinutes(5));
                AddFixedWindowPolicy(options, configuration, "broadcasts", "Broadcasts", 10, TimeSpan.FromMinutes(1));
                AddFixedWindowPolicy(options, configuration, "email-delivery", "EmailDelivery", 3, TimeSpan.FromMinutes(10));
                AddFixedWindowPolicy(options, configuration, "credentials", "Credentials", 10, TimeSpan.FromMinutes(10));
                AddFixedWindowPolicy(options, configuration, "maps", "Maps", 60, TimeSpan.FromMinutes(1));
                AddFixedWindowPolicy(options, configuration, "support", "Support", 20, TimeSpan.FromMinutes(5));
                AddFixedWindowPolicy(options, configuration, "writes", "Writes", 30, TimeSpan.FromMinutes(1), mutationsOnly: true);
            });
            return services;
        }

        public static IServiceCollection RegisterForwardedHeaders(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.ForwardLimit = Math.Max(
                    1,
                    configuration.GetValue<int?>("ReverseProxy:ForwardLimit") ?? 1);

                foreach (var value in configuration.GetSection("ReverseProxy:KnownProxies").Get<string[]>() ?? [])
                {
                    if (!IPAddress.TryParse(value, out var address))
                        throw new InvalidOperationException($"Invalid trusted proxy IP address: {value}");
                    options.KnownProxies.Add(address);
                }

                foreach (var value in configuration.GetSection("ReverseProxy:KnownIPNetworks").Get<string[]>() ?? [])
                {
                    if (!System.Net.IPNetwork.TryParse(value, out var network))
                        throw new InvalidOperationException($"Invalid trusted proxy CIDR: {value}");
                    options.KnownIPNetworks.Add(network);
                }
            });

            return services;
        }

        private static void AddFixedWindowPolicy(
            RateLimiterOptions options,
            IConfiguration configuration,
            string policyName,
            string configurationName,
            int defaultLimit,
            TimeSpan defaultWindow,
            bool mutationsOnly = false)
        {
            var permitLimit = configuration.GetValue<int?>(
                $"RateLimits:{configurationName}:PermitLimit") ?? defaultLimit;
            var windowSeconds = configuration.GetValue<int?>(
                $"RateLimits:{configurationName}:WindowSeconds") ?? (int)defaultWindow.TotalSeconds;
            options.AddPolicy(policyName, context =>
            {
                if (mutationsOnly &&
                    (HttpMethods.IsGet(context.Request.Method) ||
                     HttpMethods.IsHead(context.Request.Method) ||
                     HttpMethods.IsOptions(context.Request.Method)))
                {
                    return RateLimitPartition.GetNoLimiter(GetRateLimitPartitionKey(context));
                }

                return RateLimitPartition.GetFixedWindowLimiter(
                    GetRateLimitPartitionKey(context),
                    _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = Math.Max(1, permitLimit),
                        Window = TimeSpan.FromSeconds(Math.Max(1, windowSeconds)),
                        QueueLimit = 0,
                        AutoReplenishment = true
                    });
            });
        }

        internal static string GetRateLimitPartitionKey(HttpContext context)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrWhiteSpace(userId))
                return $"user:{userId}";

            var address = context.Connection.RemoteIpAddress;
            return address is null ? "ip:unknown" : $"ip:{address.MapToIPv6()}";
        }

        public static IServiceCollection RegisterIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = true;


            }).AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, AppDbContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole, AppDbContext, Guid>>();
            return services;
        }

        public static IServiceCollection RegisterJwtToken(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = configuration["jwt:Audience"],
                    ValidateIssuer = true,
                    ValidIssuer = configuration["jwt:Issuer"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:Key"] ??
                    throw new ArgumentNullException("No signing key found")))
                };
            });
            services.AddAuthorization();
            return services;
        }


        public static IServiceCollection RegisterHttpLogging(this IServiceCollection services)
        {
            services.AddHttpLogging(options =>
             {
                 options.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders | HttpLoggingFields.ResponsePropertiesAndHeaders;
             });
            return services;
        }


        public static IServiceCollection RegisterControllers(this IServiceCollection services)
        {
            services.AddScoped<ConditionalJsonResultFilter>();
            services.AddControllers(controller =>
            {

                controller.Filters.Add<ConditionalJsonResultFilter>();

                var policy = new AuthorizationPolicyBuilder()
               .RequireAuthenticatedUser()
               .Build();

                controller.Filters.Add(new AuthorizeFilter(policy));
            })
         .AddJsonOptions(options =>
                {

                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                 });
            return services;

        }

        public static IServiceCollection RegisterSignalR(this IServiceCollection services, IConfiguration configuration)
        {
            var permitLimit = configuration.GetValue<int?>("RateLimits:Messaging:PermitLimit") ?? 60;
            var windowSeconds = configuration.GetValue<int?>("RateLimits:Messaging:WindowSeconds") ?? 60;

            services.AddSingleton<PartitionedRateLimiter<string>>(_ =>
                PartitionedRateLimiter.Create<string, string>(context =>
                    RateLimitPartition.GetFixedWindowLimiter(context, _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = Math.Max(1, permitLimit),
                        Window = TimeSpan.FromSeconds(Math.Max(1, windowSeconds)),
                        QueueLimit = 0,
                        AutoReplenishment = true
                    })));

            services.AddSignalR(options =>
            {
                options.AddFilter<RateLimitHubFilter>();
            });

            return services;
        }

        public static IServiceCollection RegisterDataBase(this IServiceCollection services, IConfiguration configuration, IHostEnvironment? environment = null)
        {
            if (environment?.IsEnvironment("Test") != true)
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                });
                services.AddScoped<DbContext>(sp => sp.GetRequiredService<AppDbContext>());
            }
            return services;
        }


        public static IServiceCollection RegisterJsonOptions(this IServiceCollection services)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            };
            jsonOptions.Converters.Add(new JsonStringEnumConverter());

            services.AddSingleton(jsonOptions);
            return services;
        }
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IListingService, ListingService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IUserReportService, UserReportService>();
            services.AddScoped<ISavedListingService, SavedListingService>();

            services.AddScoped<IPickupLocationService, PickupLocationService>();
            services.AddScoped<ISearchQueryService, SearchQueryService>();
            services.AddScoped<IJobListingService, JobListingService>();
            services.AddScoped<ITicketAttachmentService, TicketAttachmentService>();
            services.AddScoped<ITicketMessageService, TicketMessageService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IUserModerationService, UserModerationService>();
            services.AddScoped<IAdminStatisticsService, AdminStatisticsService>();
            services.AddScoped<IAdminAuditService, AdminAuditService>();
            services.AddScoped<IListingPurgeService, ListingPurgeService>();
            services.AddHostedService<ListingPurgeBackgroundService>();

            services.AddHttpClient<GoogleMapsService>(client =>
            {
                client.Timeout = TimeSpan.FromSeconds(10);
            });
            services.AddScoped<IEmailService, EmailService>();



            services.AddScoped<INotificationService, NotificationService>();
            //services.AddScoped<IUserActivityLogService, UserActivityLogService>();
            //services.AddScoped<IUserPreferenceService, UserPreferenceService>();
            return services;

        }
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IListingRepository, ListingRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IUserReportRepository, UserReportRepository>();
            services.AddScoped<ISavedListingRepository, SavedListingRepository>();

            services.AddScoped<IPickupLocationRepository, PickupLocationRepository>();
            services.AddScoped<ISearchQueryRepository, SearchQueryRepository>();
            services.AddScoped<IJobListingRepository, JobListingRepository>();
            services.AddScoped<ITicketAttachmentRepository, TicketAttachmentRepository>();
            services.AddScoped<ITicketMessageRepository, TicketMessageRepository>();
            services.AddScoped<IListingOwnedRepository, ListingOwnedRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IUserActivityLogRepository, UserActivityLogRepository>();

            services.AddScoped<INotificationRepository, NotificationRepository>();
            //services.AddScoped<IUserActivityLogRepository, UserActivityLogRepository>();
            //services.AddScoped<IUserPreferenceRepository, UserPreferenceRepository>();
            return services;

        }

        public static IServiceCollection RegisterStorageServices(this IServiceCollection services)
        {
            services.AddSingleton<IR2ObjectStorage, R2ObjectStorage>();
            services.AddScoped<IImageUploader, ImageUploader>();
            return services;
        }
        public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration configuration, IHostEnvironment? environment = null)
        {
            return services.RegisterGoogleAuthClient(configuration, environment)
                .RegisterCors(configuration, environment)
                            .RegisterForwardedHeaders(configuration)
                            .RegisterApiVersioning()
                            .RegisterHealthAndRateLimiting(configuration)
                            .RegisterIdentity()
                            .RegisterJwtToken(configuration)
                            .RegisterControllers()
                            .RegisterSignalR(configuration)
                            .RegisterDataBase(configuration, environment)
                            .RegisterHttpLogging()
                            .RegisterJsonOptions()
                            .RegisterStorageServices()
                            .RegisterServices()
                            .RegisterRepositories();
        }
    }
}
