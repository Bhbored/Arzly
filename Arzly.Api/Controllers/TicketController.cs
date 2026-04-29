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
            _logger.LogInformation("{Controller}.GetAll - Before",
                GetType().Name);

            var result = await _service.GetAllAsync();

            _logger.LogInformation("{Controller}.GetAll - After",
                GetType().Name);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TicketResponse>> GetById(Guid id)
        {
            _logger.LogInformation("{Controller}.GetById({Id}) - Before",
                GetType().Name, id);

            var result = await _service.GetByIdAsync(id);

            _logger.LogInformation("{Controller}.GetById({Id}) - After",
                GetType().Name, id);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<TicketResponse>> Create([FromBody] TicketAddRequest createDto)
        {
            _logger.LogInformation("{Controller}.Create - Before",
                GetType().Name);

            var result = await _service.CreateAsync(createDto);

            _logger.LogInformation("{Controller}.Create - After",
                GetType().Name);
            return CreatedAtAction(nameof(GetById), new { id = result?.Id }, result);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<TicketResponse>> Update([FromBody] TicketUpdateRequest updateDto)
        {
            _logger.LogInformation("{Controller}.Update({Id}) - Before",
                GetType().Name, updateDto);

            var result = await _service.UpdateAsync(updateDto);

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
