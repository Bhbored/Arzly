using Arzly.Api.Application.Contracts;
using Arzly.Api.Filters.ActionFilters;
using Arzly.Shared.DTOs.Request.UserPreference;
using Arzly.Shared.DTOs.Response.UserPreference;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers
{
    [ApiController]
    [Route("arzly/[controller]")]
    public class UserPreferenceController : ControllerBase
    {
        private readonly ILogger<UserPreferenceController> _logger;
        private readonly IUserPreferenceService _service;

        public UserPreferenceController(IUserPreferenceService service, ILogger<UserPreferenceController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserPreferenceResponse>> GetByUserId(string userId)
        {
            try
            {
                _logger.LogInformation("{Controller}.GetByUserId({Id}) - Before", GetType().Name, userId);
                var result = await _service.GetByIdAsync(userId);
                _logger.LogInformation("{Controller}.GetByUserId({Id}) - After", GetType().Name, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UserPreferenceController.GetByUserId");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("[action]/{userId}")]
        [TypeFilter(typeof(ModelBindingFilter), Arguments = new object[] { typeof(UserPreferenceController) })]
        public async Task<ActionResult<UserPreferenceResponse?>> Update([FromBody] UserPreferenceUpdateRequest? request)
        {
            try
            {
                _logger.LogInformation("{Controller}.Update({Id}) - Before", GetType().Name, request?.UserId);
                var result = await _service.UpdateAsync(request);
                _logger.LogInformation("{Controller}.Update({Id}) - After", GetType().Name, request?.UserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UserPreferenceController.Update");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
