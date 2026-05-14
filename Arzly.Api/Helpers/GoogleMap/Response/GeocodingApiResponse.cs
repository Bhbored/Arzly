using Arzly.Api.Helpers.GoogleMap.Models;
using System.Text.Json.Serialization;

namespace Arzly.Api.Helpers.GoogleMap.Response
{
    public class GeocodingApiResponse
    {
        [JsonPropertyName("results")]
        public List<GeocodingResult> Results { get; set; } = new();

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
