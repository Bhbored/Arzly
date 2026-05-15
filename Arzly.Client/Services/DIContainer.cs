using Arzly.Client.Services.ApiClients;
using Arzly.Client.Services.Contracts;
using Microsoft.AspNetCore.HttpLogging;

namespace Arzly.Client.Services
{

    public static class DIContainer
    {

        public static IServiceCollection RegisterHttpLogging(this IServiceCollection services)
        {
            services.AddHttpLogging(options =>
            {
                options.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders | HttpLoggingFields.ResponsePropertiesAndHeaders;
            });
            return services;
        }

        public static IServiceCollection RegisterHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["ApiSettings:BaseUrl"]
                ?? throw new InvalidOperationException("No base URL is set");

            Action<HttpClient> configureClient = client =>
            {
                client.BaseAddress = new Uri(url);
                client.Timeout = TimeSpan.FromSeconds(30);
            };

            services.AddHttpClient<IListingApiClient, ListingApiClient>(configureClient)
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    MaxConnectionsPerServer = 10,
                    AllowAutoRedirect = true
                })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            services.AddHttpClient<IJobListingApiClient, JobListingApiClient>(configureClient)
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    MaxConnectionsPerServer = 10,
                    AllowAutoRedirect = true
                })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            services.AddHttpClient<ICategoryApiClient, CategoryApiClient>(configureClient)
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    MaxConnectionsPerServer = 10,
                    AllowAutoRedirect = true
                })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            services.AddHttpClient<IReportApiClient, ReportApiClient>(configureClient)
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    MaxConnectionsPerServer = 10,
                    AllowAutoRedirect = true
                })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            return services;
        }


        public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            return services.RegisterHttpLogging()
                           .RegisterHttpClient(configuration)
                           .AddSingleton<RightSliderService>()
                           .AddSingleton<ToastService>();
        }
    }
}

