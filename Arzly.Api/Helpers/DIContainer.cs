using Arzly.Api.Application.Contracts.Auth;
using Arzly.Api.Application.Contracts.Categories;
using Arzly.Api.Application.Contracts.Communications;
using Arzly.Api.Application.Contracts.Listings;
using Arzly.Api.Application.Contracts.Locations;
using Arzly.Api.Application.Contracts.Support;
using Arzly.Api.Application.Contracts.Users;
using Arzly.Api.Application.Services.Auth;
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
using Arzly.Api.Filters.ExceptionFilters;
using Arzly.Api.Filters.ResultFilters;
using Arzly.Api.Helpers.GoogleMap;
using Arzly.Api.Hubs.Contracts;
using Arzly.Api.Hubs.Services;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Api.Infrastructure.Identity;
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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arzly.Api.Helpers
{
    public static class DIContainer
    {

        public static IServiceCollection RegisterApiVersioning(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAuthentication().AddGoogleOpenIdConnect(googleOptions =>
            {
                googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
            });
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

        public static IServiceCollection RegisterCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Blazor", policy =>
                {
                    policy.WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>()
                        ?? throw new ArgumentNullException("no origins added"));
                    policy.AllowCredentials();
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
            });
            return services;
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

                controller.Filters.Add<HandleExceptionFilter>();
                controller.Filters.Add<ConditionalJsonResultFilter>();

                var policy = new AuthorizationPolicyBuilder()
               .RequireAuthenticatedUser()
               .Build();

                controller.Filters.Add(new AuthorizeFilter(policy));
            })
         .AddJsonOptions(options =>
                {

                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
        .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;//for built-in modelBinding i did a custom filter for that although never again!!
        });
            return services;

        }

        public static IServiceCollection RegisterDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<DbContext>(sp => sp.GetRequiredService<AppDbContext>());
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
            services.AddScoped<IChatMessageService, ChatMessageService>();
            services.AddScoped<IPickupLocationService, PickupLocationService>();
            services.AddScoped<ISearchQueryService, SearchQueryService>();
            services.AddScoped<IJobListingService, JobListingService>();
            services.AddScoped<ITicketAttachmentService, TicketAttachmentService>();
            services.AddScoped<ITicketMessageService, TicketMessageService>();
            services.AddScoped<IUserProfileService, UserProfileService>();

            services.AddHttpClient<GoogleMapsService>();
            services.AddScoped<IEmailService, EmailService>();



            //services.AddScoped<INotificationService, NotificationService>();
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
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddScoped<IPickupLocationRepository, PickupLocationRepository>();
            services.AddScoped<ISearchQueryRepository, SearchQueryRepository>();
            services.AddScoped<IJobListingRepository, JobListingRepository>();
            services.AddScoped<ITicketAttachmentRepository, TicketAttachmentRepository>();
            services.AddScoped<ITicketMessageRepository, TicketMessageRepository>();
            services.AddScoped<IListingOwnedRepository, ListingOwnedRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();

            //services.AddScoped<INotificationRepository, NotificationRepository>();
            //services.AddScoped<IUserActivityLogRepository, UserActivityLogRepository>();
            //services.AddScoped<IUserPreferenceRepository, UserPreferenceRepository>();
            return services;

        }

        public static IServiceCollection RegisterStorageServices(this IServiceCollection services)
        {
            services.AddScoped<ImageUploader>();
            return services;
        }
        public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            return services.RegisterCors(configuration)
                            .RegisterApiVersioning()
                            .RegisterIdentity()
                            .RegisterJwtToken(configuration)
                            .RegisterControllers()
                            .RegisterDataBase(configuration)
                            .RegisterHttpLogging()
                            .RegisterJsonOptions()
                            .RegisterStorageServices()
                            .RegisterServices()
                            .RegisterRepositories();
        }
    }
}
