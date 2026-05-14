using System.Text.Json.Serialization;

namespace Arzly.Api.Helpers.GoogleMap.Models
{
    public class DisplayName
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
