using Arzly.Api.Application.Contracts;
using Arzly.Api.Helpers.GoogleMap;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers.v1.Locations
{
    public class LocationController : CustomeControllerBase
    {
        private readonly GoogleMapsService _googleMaps;
        private readonly ILogger<LocationController> _logger;
        public LocationController(GoogleMapsService googleMaps, ILogger<LocationController> logger)
        {
            _googleMaps = googleMaps;
            _logger = logger;
        }


        [HttpGet("autocomplete")]
        public async Task<IActionResult> Autocomplete([FromQuery] string input)
        {
            _logger.LogInformation("{Controller}.Autocomplete({Input}) - Before", GetType().Name, input);


            if (string.IsNullOrWhiteSpace(input))
                return BadRequest(new { error = "Input is required." });

            var results = await _googleMaps.AutocompleteAsync(input);
            _logger.LogInformation("{Controller}.Autocomplete({Input}) - After", GetType().Name, input);
            return Ok(results);
        }


        [HttpGet("place-details")]
        public async Task<IActionResult> GetPlaceDetails([FromQuery] string placeId )
        {
            _logger.LogInformation("{Controller}.GetPlaceDetails({PlaceId}) - Before", GetType().Name, placeId);


            if (string.IsNullOrWhiteSpace(placeId))
                return BadRequest(new { error = "Place ID is required." });

            var result = await _googleMaps.GetPlaceDetailsAsync(placeId);
            _logger.LogInformation("{Controller}.GetPlaceDetails({PlaceId}) - After", GetType().Name, placeId);
            return Ok(result);
        }

        [HttpGet("reverse-geocode")]
        public async Task<IActionResult> ReverseGeocode([FromQuery] double lat, [FromQuery] double lng)
        {
            _logger.LogInformation("{Controller}.ReverseGeocode({Lat}, {Lng}) - Before", GetType().Name, lat, lng);

            var result = await _googleMaps.ReverseGeocodeAsync(lat, lng);
            _logger.LogInformation("{Controller}.ReverseGeocode({Lat}, {Lng}) - After", GetType().Name, lat, lng);
            return Ok(result);
        }


        [HttpGet("static-map")]
        public IActionResult GetStaticMap(
            [FromQuery] double lat,
            [FromQuery] double lng,
            [FromQuery] int zoom = 14,
            [FromQuery] int width = 600,
            [FromQuery] int height = 300)
        {
            _logger.LogInformation("{Controller}.GetStaticMap({Lat}, {Lng}) - Before", GetType().Name, lat, lng);
            var url = _googleMaps.GetStaticMapUrl(lat, lng, zoom, width, height);
            _logger.LogInformation("{Controller}.GetStaticMap({Lat}, {Lng}) - After", GetType().Name, lat, lng);
            return Ok(new { staticMapUrl = url });
        }
    }
}
