using Arzly.Api.Helpers.GoogleMap.Models;
using System.Text.Json.Serialization;

namespace Arzly.Api.Helpers.GoogleMap.Response
{
    public class AutocompleteResponse
    {
        [JsonPropertyName("suggestions")]
        public List<Suggestion> Suggestions { get; set; } = new();
    }
}
