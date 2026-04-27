using Arzly.Api.Application.Contracts;
using Arzly.Api.Domain.Entities;
using Arzly.Api.Filters.ActionFilters;
using Arzly.Shared.DTOs.Request.Listing;
using Arzly.Shared.DTOs.Response.Listing;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers
{

    public class ListingController : BaseController<Listing, ListingResponse, ListingAddRequest, ListingUpdateRequest, Guid>
    {
        public ListingController(IListingService listingService, ILogger<ListingController> logger)
            : base(listingService, logger)
        {
        }

        [HttpGet]
        public override async Task<ActionResult<List<ListingResponse>>> GetAll()
        {
            try
            {
                return await base.GetAll();
            }
            catch (Exception)
            {
                _logger.LogWarning("{Controller}.{Method}) - Not Found",
                    nameof(ListingController), nameof(GetAll));
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }
        [HttpPost]
        [TypeFilter(typeof(ModelBindingFilter), Arguments = new object[] { typeof(ListingController), "createDto" })]
        public override async Task<ActionResult<ListingResponse>> Create(ListingAddRequest createDto)
        {
            return await base.Create(createDto);
        }

    }
}
