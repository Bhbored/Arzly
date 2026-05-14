using Arzly.Api.Application.Contracts;
using Arzly.Api.Filters.ResultFilters;
using Arzly.Shared.DTOs.Response.Listing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers.Admin
{
    [Route("arzly/admin/[controller]")]
    [ApiController]
    public class ListingAdminController : ControllerBase
    {
        private readonly ILogger<ListingAdminController> _logger;
        private readonly IListingService _service;
        public ListingAdminController(ILogger<ListingAdminController> logger, IListingService service)
        {
            _service = service;
            _logger = logger;
        }
        //admin & support later
        [HttpGet("get-all")]
        public async Task<ActionResult<List<ListingResponse>>> GetAll()
        {
            _logger.LogInformation("{Controller}.GetAll - Before",
                GetType().Name);

            var result = await _service.GetAllAsync();

            _logger.LogInformation("{Controller}.GetAll - After",
                GetType().Name);
            return result;
        }
    }
}
