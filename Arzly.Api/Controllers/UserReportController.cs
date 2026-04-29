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
            _logger.LogInformation("{Controller}.GetAll - Before",
                GetType().Name);

            var result = await _service.GetAllAsync();

            _logger.LogInformation("{Controller}.GetAll - After",
                GetType().Name);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserReportResponse>> GetById(Guid id)
        {
            _logger.LogInformation("{Controller}.GetById({Id}) - Before",
                GetType().Name, id);

            var result = await _service.GetByIdAsync(id);

            _logger.LogInformation("{Controller}.GetById({Id}) - After",
                GetType().Name, id);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<UserReportResponse>> Create([FromBody] UserReportAddRequest createDto)
        {
            _logger.LogInformation("{Controller}.Create - Before",
                GetType().Name);

            var result = await _service.CreateAsync(createDto);

            _logger.LogInformation("{Controller}.Create - After",
                GetType().Name);
            return CreatedAtAction(nameof(GetById), new { id = result?.Id }, result);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<UserReportResponse>> Update([FromBody] UserReportUpdateRequest updateDto)
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
