using Arzly.Api.Application.Contracts.Users;
using Arzly.Api.Filters.ResultFilters;
using Arzly.Shared.DTOs.Request.UserProfile;
using Arzly.Shared.DTOs.Response.UserProfile;
using Arzly.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers.v1.Users
{
    [JsonFormatter(UsePascalCase = true)]
    public class UserProfileController : CustomeControllerBase
    {
        private readonly IUserProfileService _service;
        private readonly ILogger<UserProfileController> _logger;

        public UserProfileController(IUserProfileService service, ILogger<UserProfileController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("{userId:guid}")]
        public async Task<ActionResult<UserProfileResponse>> GetCurrent(Guid userId)
        {
            _logger.LogInformation("{Controller}.GetCurrent({UserId}) - Before",
                GetType().Name, userId);

            var result = await _service.GetByIdAsync(userId);

            _logger.LogInformation("{Controller}.GetCurrent({UserId}) - After",
                GetType().Name, userId);
            return Ok(result);
        }

        [HttpGet("me")]
        public async Task<ActionResult<UserProfileResponse>> GetCurrent()
        {
            var userId = User.GetUserId();
            _logger.LogInformation("{Controller}.GetCurrent({UserId}) - Before",
                GetType().Name, userId);

            var result = await _service.GetByIdAsync(userId);

            _logger.LogInformation("{Controller}.GetCurrent({UserId}) - After",
                GetType().Name, userId);
            return Ok(result);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<UserProfileResponse>> Update([FromBody] UserProfileUpdateRequest updateDto)
        {
            _logger.LogInformation("{Controller}.Update({UserId}) - Before",
                GetType().Name, User.GetUserId());

            var result = await _service.UpdateAsync(updateDto, User.GetUserId());

            _logger.LogInformation("{Controller}.Update({UserId}) - After",
                GetType().Name, User.GetUserId());
            return Ok(result);
        }
    }
}
