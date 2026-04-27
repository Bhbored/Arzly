using Arzly.Api.Application.Contracts;
using Arzly.Shared.DTOs.Request.UserReport;
using Arzly.Shared.DTOs.Response.UserReport;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers
{
    [ApiController]
    [Route("arzly/[controller]")]
    public class UserReportController : ControllerBase
    {
        private readonly IUserReportService _service;
        private readonly ILogger<UserReportController> _logger;

        public UserReportController(IUserReportService service, ILogger<UserReportController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserReportResponse>>> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UserReportController.GetAll");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserReportResponse>> GetById(Guid id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UserReportController.GetById");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<UserReportResponse>> Create([FromBody] UserReportAddRequest createDto)
        {
            try
            {
                var result = await _service.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = result?.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UserReportController.Create");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<UserReportResponse>> Update([FromBody] UserReportUpdateRequest updateDto)
        {
            try
            {
                var result = await _service.UpdateAsync(updateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UserReportController.Update");
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
                _logger.LogError(ex, "Error in UserReportController.Delete");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
