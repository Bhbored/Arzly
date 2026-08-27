using System.Text.Json.Serialization;

namespace Arzly.Api.Helpers.GoogleMap.Models
{
    public class PlacePrediction
    {
        [JsonPropertyName("placeId")]
        public string PlaceId { get; set; } = null!;

        [JsonPropertyName("text")]
        public TextWithMatches Text { get; set; } = null!;

        [JsonPropertyName("structuredFormat")]
        public StructuredFormat StructuredFormat { get; set; } = null!;
    }
}
