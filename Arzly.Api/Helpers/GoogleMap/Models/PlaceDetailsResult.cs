namespace Arzly.Api.Helpers.GoogleMap.Models
{
    public class PlaceDetailsResult
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string FormattedAddress { get; set; } = string.Empty;
        public string StaticMapUrl { get; set; } = string.Empty;
    }
}
