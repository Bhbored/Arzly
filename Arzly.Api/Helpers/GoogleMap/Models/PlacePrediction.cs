using System.Text.Json.Serialization;

namespace Arzly.Api.Helpers.GoogleMap.Models
{
    public class PlacePrediction
    {
        [JsonPropertyName("placeId")]
        public string PlaceId { get; set; }

        [JsonPropertyName("text")]
        public TextWithMatches Text { get; set; }

        [JsonPropertyName("structuredFormat")]
        public StructuredFormat StructuredFormat { get; set; }
    }
}
