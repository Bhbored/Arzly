using Arzly.Api.Application.Contracts;
using Arzly.Shared.DTOs.Request.SavedListing;
using Arzly.Shared.DTOs.Response.SavedListing;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers
{
    [ApiController]
    [Route("arzly/[controller]")]
    public class SavedListingController : ControllerBase
    {
        private readonly ISavedListingService _service;
        private readonly ILogger<SavedListingController> _logger;

        public SavedListingController(ISavedListingService service, ILogger<SavedListingController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<SavedListingResponse>>> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SavedListingController.GetAll");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SavedListingResponse>> GetById(Guid id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SavedListingController.GetById");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<SavedListingResponse>> Create([FromBody] SavedListingAddRequest createDto)
        {
            try
            {
                var result = await _service.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = result?.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SavedListingController.Create");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<SavedListingResponse>> Update([FromBody] SavedListingUpdateRequest updateDto)
        {
            try
            {
                var result = await _service.UpdateAsync(updateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SavedListingController.Update");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var success = await _service.DeleteAsync(id);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SavedListingController.Delete");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
