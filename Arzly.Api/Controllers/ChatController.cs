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
            _logger.LogInformation("{Controller}.GetAll - Before",
                GetType().Name);

            var result = await _service.GetAllAsync();

            _logger.LogInformation("{Controller}.GetAll - After",
                GetType().Name);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ChatResponse>> GetById(Guid id)
        {
            _logger.LogInformation("{Controller}.GetById({Id}) - Before",
                GetType().Name, id);

            var result = await _service.GetByIdAsync(id);

            _logger.LogInformation("{Controller}.GetById({Id}) - After",
                GetType().Name, id);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ChatResponse>> Create([FromBody] ChatAddRequest createDto, [FromHeader] string? userId)
        {
            _logger.LogInformation("{Controller}.Create - Before",
                GetType().Name);

            var result = await _service.CreateAsync(createDto, userId);

            _logger.LogInformation("{Controller}.Create - After",
                GetType().Name);
            return CreatedAtAction(nameof(GetById), new { id = result?.Id }, result);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<ChatResponse>> Update([FromBody] ChatUpdateRequest updateDto, [FromHeader] string? userId)
        {
            _logger.LogInformation("{Controller}.Update({Id}) - Before",
                GetType().Name, updateDto);

            var result = await _service.UpdateAsync(updateDto,userId);

            _logger.LogInformation("{Controller}.Update({Id}) - After",
                GetType().Name, updateDto);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            _logger.LogInformation("{Controller}.Delete({Id}) - Before",
                GetType().Name, id);

            await _service.DeleteAsync(id);

            _logger.LogInformation("{Controller}.Delete({Id}) - After",
                GetType().Name, id);
            return NoContent();
        }
    }
}
