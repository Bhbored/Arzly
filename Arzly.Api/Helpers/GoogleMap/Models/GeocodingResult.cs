using System.Text.Json.Serialization;

namespace Arzly.Api.Helpers.GoogleMap.Models
{
    public class GeocodingResult
    {
        [JsonPropertyName("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonPropertyName("address_components")]
        public List<AddressComponent> AddressComponents { get; set; } = new();
    }
}
