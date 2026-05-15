using Arzly.Api.Application.Contracts;
using Arzly.Api.Filters.ActionFilters;
using Arzly.Api.Filters.ResultFilters;
using Arzly.Shared.DTOs.Request.Listing;
using Arzly.Shared.DTOs.Response.Listing;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<List<ListingResponse>>> GetAll([FromHeader] int pageSize = 10, [FromHeader] int currentPage = 0)
        {
            _logger.LogInformation("{Controller}.GetAll - Before",
                GetType().Name);

            var result = await _service.GetAllListingAdmin(pageSize,currentPage);

            _logger.LogInformation("{Controller}.GetAll - After",
                GetType().Name);
            return result;
        }

        [HttpPut("[action]")]
        [TypeFilter(typeof(ModelBindingFilter), Arguments = new object[] { typeof(ListingController) })]
        public async Task<ActionResult<ListingResponse?>> Update([FromBody] ListingUpdateRequest? request)
        {
            _logger.LogInformation("{Controller}.Update({Id}) - Before",
                GetType().Name, request);

            var result = await _service.UpdateAsyncAdmin(request);

            _logger.LogInformation("{Controller}.Update({Id}) - After",
                GetType().Name, request);
            return Ok(result);
        }
    }
}
