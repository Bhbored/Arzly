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

    protected async Task<T?> GetAsync<T>(string url, Dictionary<string, string>? headers = null)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            var response = await HttpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>();
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
    protected async Task<TResponse?> PutAsync<TRequest, TResponse>(string url, TRequest request)
    {
        try
        {
            var response = await HttpClient.PutAsJsonAsync(url, request);
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
