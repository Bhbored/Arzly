using Arzly.Api.Application.Contracts;
using Arzly.Shared.DTOs.Request.Ticket;
using Arzly.Shared.DTOs.Response.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers
{
    [ApiController]
    [Route("arzly/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _service;
        private readonly ILogger<TicketController> _logger;

        public TicketController(ITicketService service, ILogger<TicketController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<TicketResponse>>> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in TicketController.GetAll");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TicketResponse>> GetById(Guid id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in TicketController.GetById");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<TicketResponse>> Create([FromBody] TicketAddRequest createDto)
        {
            try
            {
                var result = await _service.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = result?.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in TicketController.Create");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<TicketResponse>> Update([FromBody] TicketUpdateRequest updateDto)
        {
            try
            {
                var result = await _service.UpdateAsync(updateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in TicketController.Update");
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
                _logger.LogError(ex, "Error in TicketController.Delete");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
