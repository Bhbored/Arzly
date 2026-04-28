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
                _logger.LogInformation("{Controller}.GetAll - Before",
                    GetType().Name);

                var result = await _service.GetAllAsync();

                _logger.LogInformation("{Controller}.GetAll - After",
                    GetType().Name);
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
                _logger.LogInformation("{Controller}.GetById({Id}) - Before",
                    GetType().Name, id);

                var result = await _service.GetByIdAsync(id);

                _logger.LogInformation("{Controller}.GetById({Id}) - After",
                    GetType().Name, id);
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
                _logger.LogInformation("{Controller}.Create - Before",
                    GetType().Name);

                var result = await _service.CreateAsync(createDto);

                _logger.LogInformation("{Controller}.Create - After",
                    GetType().Name);
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
                _logger.LogInformation("{Controller}.Update({Id}) - Before",
                    GetType().Name, updateDto);

                var result = await _service.UpdateAsync(updateDto);

                _logger.LogInformation("{Controller}.Update({Id}) - After",
                    GetType().Name, updateDto);
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
                _logger.LogInformation("{Controller}.Delete({Id}) - Before",
                    GetType().Name, id);

                var success = await _service.DeleteAsync(id);

                _logger.LogInformation("{Controller}.Delete({Id}) - After",
                    GetType().Name, id);
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
