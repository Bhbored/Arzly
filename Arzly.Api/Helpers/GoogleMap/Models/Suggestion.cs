using System.Text.Json.Serialization;

namespace Arzly.Api.Helpers.GoogleMap.Models
{
    public class Suggestion
    {
        [JsonPropertyName("placePrediction")]
        public PlacePrediction PlacePrediction { get; set; } = null!;
    }
}
