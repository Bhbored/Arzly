namespace Arzly.Api.Helpers.GoogleMap.Models
{
    public class PlaceDetailsResult
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string DisplayName { get; set; }
        public string FormattedAddress { get; set; }
        public string StaticMapUrl { get; set; }
    }
}
