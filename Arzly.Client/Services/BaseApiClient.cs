using System.Net.Http.Json;

namespace Arzly.Client.Services;

public abstract class BaseApiClient
{
    protected readonly HttpClient HttpClient;
    protected readonly ILogger<BaseApiClient> Logger;

    protected BaseApiClient(HttpClient httpClient, ILogger<BaseApiClient> logger)
    {
        HttpClient = httpClient;
        Logger = logger;
    }

    protected async Task<T?> GetAsync<T>(string url)
    {
        try
        {
            return await HttpClient.GetFromJsonAsync<T>(url);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error fetching data from {Url}", url);
            return default;
        }
    }

    protected async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest request)
    {
        try
        {
            var response = await HttpClient.PostAsJsonAsync(url, request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TResponse>();
            }
            return default;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error posting data to {Url}", url);
            return default;
        }
    }
}
