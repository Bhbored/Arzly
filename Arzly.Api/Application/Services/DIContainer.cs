using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Contracts;
using Arzly.Api.Infrastructure.Repositories;

namespace Arzly.Api.Application.Services
{
    public static class DIContainer
    {

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IListingService, ListingService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IUserReportService, UserReportService>();
            services.AddScoped<ISavedListingService, SavedListingService>();
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
            return services;

        }
        public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            return services.RegisterServices()
                           .RegisterRepositories();
        }
    }
}
