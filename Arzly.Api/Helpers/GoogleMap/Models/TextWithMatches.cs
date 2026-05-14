using System.Text.Json.Serialization;

namespace Arzly.Api.Helpers.GoogleMap.Models
{
    public class TextWithMatches
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
