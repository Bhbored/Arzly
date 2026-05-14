using System.Text.Json.Serialization;

namespace Arzly.Api.Helpers.GoogleMap.Models
{
    public class Location
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }
}
