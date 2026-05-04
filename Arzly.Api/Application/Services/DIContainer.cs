using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Api.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arzly.Api.Application.Services
{
    public static class DIContainer
    {

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
            services.AddScoped<IUserService, UserService>();


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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IListingOwnedRepository, ListingOwnedRepository>();

            //services.AddScoped<INotificationRepository, NotificationRepository>();
            //services.AddScoped<IUserActivityLogRepository, UserActivityLogRepository>();
            //services.AddScoped<IUserPreferenceRepository, UserPreferenceRepository>();
            return services;

        }
        public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            return services.RegisterDataBase(configuration)
                            .RegisterJsonOptions()
                           .RegisterServices()
                           .RegisterRepositories();
        }
    }
}
