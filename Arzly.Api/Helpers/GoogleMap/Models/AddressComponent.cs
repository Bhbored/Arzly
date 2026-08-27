using System.Text.Json.Serialization;

namespace Arzly.Api.Helpers.GoogleMap.Models
{
    public class AddressComponent
    {
        [JsonPropertyName("long_name")]
        public string LongName { get; set; } = null!;

        [JsonPropertyName("short_name")]
        public string ShortName { get; set; } = null!;

        [JsonPropertyName("types")]
        public List<string> Types { get; set; } = new();
    }
}
