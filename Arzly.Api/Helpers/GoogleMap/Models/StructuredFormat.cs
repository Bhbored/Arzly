using System.Text.Json.Serialization;

namespace Arzly.Api.Helpers.GoogleMap.Models
{
    public class StructuredFormat
    {
        [JsonPropertyName("mainText")]
        public TextWithMatches MainText { get; set; }

        [JsonPropertyName("secondaryText")]
        public TextWithMatches SecondaryText { get; set; }
    }
}
