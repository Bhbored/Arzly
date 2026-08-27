using Arzly.Api.Application.Contracts;
using Arzly.Api.Application.Contracts.Listings;
using Arzly.Api.Filters.ResultFilters;
using Arzly.Shared.DTOs.Request.SavedListing;
using Arzly.Shared.DTOs.Response.SavedListing;
using Arzly.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers.v1.Listings
{
    [JsonFormatter(UsePascalCase = true)]

    public class SavedListingController : CustomeControllerBase
    {
        private readonly ISavedListingService _service;
        private readonly ILogger<SavedListingController> _logger;

        public SavedListingController(ISavedListingService service, ILogger<SavedListingController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("user-saved-listings")]
        public async Task<ActionResult<List<SavedListingResponse>>> GetUserSavedListings()
        {
            _logger.LogInformation("{Controller}.GetAll - Before",
                GetType().Name);

            var result = await _service.GetAllAsync(User.GetUserId());

            _logger.LogInformation("{Controller}.GetAll - After",
                GetType().Name);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SavedListingResponse>> GetById(Guid id)
        {
            _logger.LogInformation("{Controller}.GetById({Id}) - Before",
                GetType().Name, id);

            var result = await _service.GetByIdAsync(id, User.GetUserId());

            _logger.LogInformation("{Controller}.GetById({Id}) - After",
                GetType().Name, id);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<SavedListingResponse>> Create([FromBody] SavedListingAddRequest createDto)
        {
            _logger.LogInformation("{Controller}.Create - Before",
                GetType().Name);

            var result = await _service.CreateAsync(createDto, User.GetUserId());

            _logger.LogInformation("{Controller}.Create - After",
                GetType().Name);
            return CreatedAtAction(nameof(GetById), new { id = result?.Id }, result);
        }

      

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            _logger.LogInformation("{Controller}.Delete({Id}) - Before",
                GetType().Name, id);

            await _service.DeleteAsync(id, User.GetUserId());

            _logger.LogInformation("{Controller}.Delete({Id}) - After",
                GetType().Name, id);
            return NoContent();
        }
    }
}
