using Arzly.Api.Helpers.GoogleMap.Models;
using System.Text.Json.Serialization;

namespace Arzly.Api.Helpers.GoogleMap.Response
{
    public class PlaceDetailsResponse
    {
        [JsonPropertyName("location")]
        public Location Location { get; set; } = null!;

        [JsonPropertyName("displayName")]
        public DisplayName DisplayName { get; set; } = null!;

        [JsonPropertyName("formattedAddress")]
        public string FormattedAddress { get; set; } = null!;
    }

}
