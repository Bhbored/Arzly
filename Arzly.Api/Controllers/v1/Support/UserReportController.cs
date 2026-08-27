using Arzly.Api.Application.Contracts;
using Arzly.Api.Application.Contracts.Users;
using Arzly.Api.Filters.ResultFilters;
using Arzly.Shared.DTOs.Request.UserReport;
using Arzly.Shared.DTOs.Response.UserReport;
using Arzly.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

namespace Arzly.Api.Controllers.v1.Support
{
    [JsonFormatter(UsePascalCase = true)]

    public class UserReportController : CustomeControllerBase
    {
        private readonly IUserReportService _service;
        private readonly ILogger<UserReportController> _logger;

        public UserReportController(IUserReportService service, ILogger<UserReportController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "admin,support")]
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

            var canModerate = User.IsInRole("admin") || User.IsInRole("support");
            var result = await _service.GetByIdAsync(id, User.GetUserId(), canModerate);

            _logger.LogInformation("{Controller}.GetById({Id}) - After",
                GetType().Name, id);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [EnableRateLimiting("reports")]
        public async Task<ActionResult<UserReportResponse>> Create([FromBody] UserReportAddRequest createDto)
        {
            _logger.LogInformation("{Controller}.Create - Before",
                GetType().Name);

            var result = await _service.CreateAsync(createDto, User.GetUserId());

            _logger.LogInformation("{Controller}.Create - After",
                GetType().Name);
            return CreatedAtAction(nameof(GetById), new { id = result?.Id }, result);
        }

        [HttpPut("[action]")]
        [Authorize(Roles = "admin,support")]
        public async Task<ActionResult<UserReportResponse>> Update([FromBody] UserReportUpdateRequest updateDto)
        {
            _logger.LogInformation("{Controller}.Update({Id}) - Before",
                GetType().Name, updateDto);

            var result = await _service.ResolveAsync(updateDto.Id, User.GetUserId(), updateDto.IsResolved);

            _logger.LogInformation("{Controller}.Update({Id}) - After",
                GetType().Name, updateDto);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "admin")]
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
