using Arzly.Api.Application.Contracts.Listings;
﻿using Arzly.Api.Application.Contracts;

using Arzly.Api.Filters.ResultFilters;
using Arzly.Shared.DTOs.Request.Listing;
using Arzly.Shared.DTOs.Response.Listing;
using Arzly.Shared.Enums.Listing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Arzly.Shared.Extensions;
using Arzly.Shared.DTOs.Response.UserActivityLog;
using Arzly.Api.Application.Contracts.Admin;
using Arzly.Shared.DTOs.Response.Admin;

namespace Arzly.Api.Controllers.Admin
{
    [Route("arzly/v{version:apiVersion}/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class ListingAdminController : ControllerBase
    {
        private readonly ILogger<ListingAdminController> _logger;
        private readonly IListingService _service;
        private readonly IListingPurgeService _purgeService;
        public ListingAdminController(
            ILogger<ListingAdminController> logger,
            IListingService service,
            IListingPurgeService purgeService)
        {
            _service = service;
            _purgeService = purgeService;
            _logger = logger;
        }
        [HttpGet("get-all")]
        public async Task<ActionResult<List<ListingResponse>>> GetAll([FromHeader] int pageSize = 10, [FromHeader] int currentPage = 0)
        {
            _logger.LogInformation("{Controller}.GetAll - Before",
                GetType().Name);

            var result = await _service.GetAllListingAdmin(pageSize,currentPage);

            _logger.LogInformation("{Controller}.GetAll - After",
                GetType().Name);
            return result;
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<ListingResponse?>> Update([FromBody] ListingUpdateRequest? request)
        {
            _logger.LogInformation("{Controller}.Update({Id}) - Before",
                GetType().Name, request);

            var result = await _service.UpdateAsyncAdmin(request);

            _logger.LogInformation("{Controller}.Update({Id}) - After",
                GetType().Name, request);
            return Ok(result);
        }

        [HttpGet("purge-preview")]
        public async Task<ActionResult<object>> GetPurgePreview(CancellationToken cancellationToken) =>
            Ok(new { eligibleListings = await _purgeService.CountEligibleAsync(cancellationToken) });

        [HttpPost("purge-expired")]
        public async Task<ActionResult<ListingPurgeResultResponse>> PurgeExpired(
            [FromQuery] int batchSize = 100,
            CancellationToken cancellationToken = default) =>
            Ok(await _purgeService.PurgeExpiredAsync(
                User.GetUserId(), "admin", batchSize, cancellationToken));

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ListingResponse>> GetById(Guid id) =>
            Ok(await _service.GetByIdAdminAsync(id));

        [HttpPut("{id:guid}/status")]
        public async Task<ActionResult<ListingResponse>> SetStatus(
            Guid id,
            [FromBody] ListingStatus status) =>
            Ok(await _service.SetStatusAdminAsync(id, status, User.GetUserId(), "admin"));

        [HttpPut("{id:guid}/reject")]
        public async Task<ActionResult<ListingResponse>> Reject(
            Guid id,
            [FromBody] ListingRejectionRequest request) =>
            Ok(await _service.RejectAdminAsync(id, request.Reason, User.GetUserId(), "admin"));

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAdminAsync(id, User.GetUserId(), "admin");
            return NoContent();
        }

        [HttpPost("{id:guid}/restore")]
        public async Task<ActionResult<ListingResponse>> Restore(Guid id) =>
            Ok(await _service.RestoreAdminAsync(id, User.GetUserId(), "admin"));

        [HttpGet("{id:guid}/history")]
        public async Task<ActionResult<List<UserActivityLogResponse>>> GetHistory(
            Guid id,
            [FromQuery] int pageSize = 20,
            [FromQuery] int currentPage = 0) =>
            Ok(await _service.GetModerationHistoryAsync(id, pageSize, currentPage));
    }
}
