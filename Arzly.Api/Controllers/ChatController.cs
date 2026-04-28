using Arzly.Api.Application.Contracts;
using Arzly.Shared.DTOs.Request.Chat;
using Arzly.Shared.DTOs.Response.Chat;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers
{
    [ApiController]
    [Route("arzly/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _service;
        private readonly ILogger<ChatController> _logger;

        public ChatController(IChatService service, ILogger<ChatController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<ChatResponse>>> GetAll()
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
                _logger.LogError(ex, "Error in ChatController.GetAll");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ChatResponse>> GetById(Guid id)
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
                _logger.LogError(ex, "Error in ChatController.GetById");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ChatResponse>> Create([FromBody] ChatAddRequest createDto)
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
                _logger.LogError(ex, "Error in ChatController.Create");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<ChatResponse>> Update([FromBody] ChatUpdateRequest updateDto)
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
                _logger.LogError(ex, "Error in ChatController.Update");
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
                _logger.LogError(ex, "Error in ChatController.Delete");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
