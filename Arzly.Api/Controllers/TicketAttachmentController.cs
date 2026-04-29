using Arzly.Api.Application.Contracts;
using Arzly.Api.Filters.ActionFilters;
using Arzly.Shared.DTOs.Request.TicketAttachment;
using Arzly.Shared.DTOs.Response.TicketAttachment;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers
{
    [ApiController]
    [Route("arzly/[controller]")]
    public class TicketAttachmentController : ControllerBase
    {
        private readonly ILogger<TicketAttachmentController> _logger;
        private readonly ITicketAttachmentService _service;

        public TicketAttachmentController(ITicketAttachmentService service, ILogger<TicketAttachmentController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public virtual async Task<ActionResult<List<TicketAttachmentResponse>>> GetAll()
        {
            _logger.LogInformation("{Controller}.GetAll - Before", GetType().Name);
            var result = await _service.GetAllAsync();
            _logger.LogInformation("{Controller}.GetAll - After", GetType().Name);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TicketAttachmentResponse>> GetById(Guid? id)
        {
            _logger.LogInformation("{Controller}.GetById({Id}) - Before", GetType().Name, id);
            var result = await _service.GetByIdAsync(id.Value);
            _logger.LogInformation("{Controller}.GetById({Id}) - After", GetType().Name, id);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [TypeFilter(typeof(ModelBindingFilter), Arguments = new object[] { typeof(TicketAttachmentController) })]
        public async Task<ActionResult<TicketAttachmentResponse>> Create([FromBody] TicketAttachmentAddRequest? request)
        {
            _logger.LogInformation("{Controller}.Create - Before", GetType().Name);
            var result = await _service.CreateAsync(request);
            _logger.LogInformation("{Controller}.Create - After", GetType().Name);
            return CreatedAtAction(nameof(GetById), new { id = result?.Id }, result);
        }

        [HttpPut("[action]/{id:guid}")]
        [TypeFilter(typeof(ModelBindingFilter), Arguments = new object[] { typeof(TicketAttachmentController) })]
        public async Task<ActionResult<TicketAttachmentResponse?>> Update([FromBody] TicketAttachmentUpdateRequest? request)
        {
            _logger.LogInformation("{Controller}.Update({Id}) - Before", GetType().Name, request);
            var result = await _service.UpdateAsync(request);
            _logger.LogInformation("{Controller}.Update({Id}) - After", GetType().Name, request);
            return Ok(result);
        }

        [HttpDelete("[action]/{id:guid}")]
        public async Task<ActionResult> Delete(Guid? id)
        {
            _logger.LogInformation("{Controller}.Delete({Id}) - Before", GetType().Name, id);
            await _service.DeleteAsync(id.Value);
            _logger.LogInformation("{Controller}.Delete({Id}) - After", GetType().Name, id);
            return NoContent();
        }
    }
}
