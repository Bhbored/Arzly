using Arzly.Api.Helpers.GoogleMap.Models;
using Arzly.Api.Helpers.GoogleMap.Response;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json.Serialization;
using System.Globalization;

namespace Arzly.Api.Helpers.GoogleMap
{
    public class GoogleMapsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly ILogger<GoogleMapsService> _logger;

        public GoogleMapsService(HttpClient httpClient, IConfiguration configuration, ILogger<GoogleMapsService> logger)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GoogleMaps:ApiKey"]
                ?? throw new ArgumentNullException("GoogleMaps:ApiKey not configured");
            _logger = logger;
        }

      
        public async Task<List<PlaceResult>> AutocompleteAsync(string input, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("{Service}.AutocompleteAsync({Input}) - Before", GetType().Name, input);

            if (string.IsNullOrWhiteSpace(input) || input.Length < 2)
            {
                _logger.LogWarning("{Service}.AutocompleteAsync - Input too short: {Length}", GetType().Name, input?.Length ?? 0);
                return new List<PlaceResult>();
            }

            var requestBody = new
            {
                input,
                includedRegionCodes = new[] { "LB" }
            };

            using var response = await SendWithRetryAsync(() =>
            {
                var request = new HttpRequestMessage(HttpMethod.Post,
                    "https://places.googleapis.com/v1/places:autocomplete");
                request.Headers.Add("X-Goog-Api-Key", _apiKey);
                request.Content = JsonContent.Create(requestBody);
                return request;
            }, cancellationToken);

            var result = await response.Content.ReadFromJsonAsync<AutocompleteResponse>(cancellationToken);

            _logger.LogInformation("{Service}.AutocompleteAsync - Found {Count} suggestions", GetType().Name, result?.Suggestions.Count ?? 0);

            return result?.Suggestions.Select(s => new PlaceResult
            {
                PlaceId = s.PlacePrediction.PlaceId,
                MainText = s.PlacePrediction.StructuredFormat.MainText.Text,
                SecondaryText = s.PlacePrediction.StructuredFormat.SecondaryText.Text,
                FullText = s.PlacePrediction.Text.Text
            }).ToList() ?? new List<PlaceResult>();
        }

      
        public async Task<PlaceDetailsResult> GetPlaceDetailsAsync(string placeId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("{Service}.GetPlaceDetailsAsync({PlaceId}) - Before", GetType().Name, placeId);

            var fieldMask = "location,displayName,formattedAddress";

            using var response = await SendWithRetryAsync(() =>
            {
                var request = new HttpRequestMessage(HttpMethod.Get,
                    $"https://places.googleapis.com/v1/places/{Uri.EscapeDataString(placeId)}");
                request.Headers.Add("X-Goog-Api-Key", _apiKey);
                request.Headers.Add("X-Goog-FieldMask", fieldMask);
                return request;
            }, cancellationToken);

            var place = await response.Content.ReadFromJsonAsync<PlaceDetailsResponse>(cancellationToken);

            if (place == null)
            {
                _logger.LogError("{Service}.GetPlaceDetailsAsync({PlaceId}) - Place not found", GetType().Name, placeId);
                throw new Exception("Failed to retrieve place details.");
            }

            _logger.LogInformation("{Service}.GetPlaceDetailsAsync({PlaceId}) - Success: {DisplayName}", GetType().Name, placeId, place.DisplayName.Text);

            return new PlaceDetailsResult
            {
                Latitude = place.Location.Latitude,
                Longitude = place.Location.Longitude,
                DisplayName = place.DisplayName.Text,
                FormattedAddress = place.FormattedAddress,
                StaticMapUrl = GetStaticMapUrl(place.Location.Latitude, place.Location.Longitude)
            };
        }

        public async Task<PlaceDetailsResult> ReverseGeocodeAsync(double lat, double lng, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("{Service}.ReverseGeocodeAsync({Lat}, {Lng}) - Before", GetType().Name, lat, lng);

            var coordinates = FormattableString.Invariant($"{lat},{lng}");
            using var response = await SendWithRetryAsync(() => new HttpRequestMessage(
                HttpMethod.Get,
                $"https://maps.googleapis.com/maps/api/geocode/json?latlng={coordinates}&key={Uri.EscapeDataString(_apiKey)}"),
                cancellationToken);

            var geocodeResult = await response.Content
                .ReadFromJsonAsync<GeocodingApiResponse>(cancellationToken);

            var result = geocodeResult?.Results?.FirstOrDefault();
            if (result == null)
            {
                _logger.LogError("{Service}.ReverseGeocodeAsync({Lat}, {Lng}) - No results found", GetType().Name, lat, lng);
                throw new Exception("No results found for these coordinates.");
            }

            var country = result.AddressComponents
                .FirstOrDefault(c => c.Types.Contains("country"))?.LongName;

            if (country != "Lebanon")
            {
                _logger.LogWarning("{Service}.ReverseGeocodeAsync({Lat}, {Lng}) - Country not Lebanon: {Country}", GetType().Name, lat, lng, country);
                throw new Exception("Location must be within Lebanon.");
            }

            _logger.LogInformation("{Service}.ReverseGeocodeAsync({Lat}, {Lng}) - Success: {DisplayName}", GetType().Name, lat, lng, ExtractDisplayName(result.AddressComponents));

            return new PlaceDetailsResult
            {
                Latitude = lat,
                Longitude = lng,
                DisplayName = ExtractDisplayName(result.AddressComponents),
                FormattedAddress = result.FormattedAddress,
                StaticMapUrl = GetStaticMapUrl(lat, lng)
            };
        }

      
        public string GetStaticMapUrl(double lat, double lng, int zoom = 14, int width = 600, int height = 300)
        {
            return $"https://maps.googleapis.com/maps/api/staticmap" +
                   $"?center={lat},{lng}" +
                   $"&zoom={zoom}" +
                   $"&size={width}x{height}" +
                   $"&markers=color:red%7C{lat},{lng}" +
                   $"&key={_apiKey}";
        }

        private string ExtractDisplayName(List<AddressComponent> components)
        {
            var locality = components.FirstOrDefault(c => c.Types.Contains("locality"))?.LongName;
            var adminArea = components.FirstOrDefault(c => c.Types.Contains("administrative_area_level_1"))?.LongName;
            var country = components.FirstOrDefault(c => c.Types.Contains("country"))?.LongName;

            return locality ?? adminArea ?? country ?? "Unknown Location";
        }

        private async Task<HttpResponseMessage> SendWithRetryAsync(
            Func<HttpRequestMessage> requestFactory,
            CancellationToken cancellationToken)
        {
            const int maximumAttempts = 3;
            for (var attempt = 1; ; attempt++)
            {
                using var request = requestFactory();
                try
                {
                    var response = await _httpClient.SendAsync(request, cancellationToken);
                    if (!IsTransient(response.StatusCode) || attempt == maximumAttempts)
                    {
                        response.EnsureSuccessStatusCode();
                        return response;
                    }

                    response.Dispose();
                    _logger.LogWarning(
                        "Google Maps returned transient status {StatusCode}; retrying attempt {NextAttempt}",
                        (int)response.StatusCode,
                        attempt + 1);
                }
                catch (HttpRequestException exception) when (
                    attempt < maximumAttempts && exception.StatusCode is null)
                {
                    _logger.LogWarning(exception,
                        "Google Maps request failed transiently; retrying attempt {NextAttempt}", attempt + 1);
                }

                await Task.Delay(TimeSpan.FromMilliseconds(200 * attempt), cancellationToken);
            }
        }

        private static bool IsTransient(System.Net.HttpStatusCode statusCode) =>
            statusCode is System.Net.HttpStatusCode.RequestTimeout or
                System.Net.HttpStatusCode.TooManyRequests || (int)statusCode >= 500;
    }


}
