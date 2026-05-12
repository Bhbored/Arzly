using Arzly.Api.Application.Contracts;
using Arzly.Api.Infrastructure.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers
{
    [ApiController]
    [Route("arzly/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly ImageUploader _imageUploader;
        private readonly IUserService _userService;
        private readonly ILogger<UploadController> _logger;

        public UploadController(ImageUploader imageUploader, IUserService userService, ILogger<UploadController> logger)
        {
            _imageUploader = imageUploader;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file, [FromHeader] string firebaseId)
        {
            _logger.LogInformation("{Controller}.UploadImage({FirebaseId}) - Before",
                GetType().Name, firebaseId);

            var user = await _userService.GetByFireBaseIdAsync(firebaseId);
            if (user != null)
            {
                using var stream = file.OpenReadStream();
                var url = await _imageUploader.UploadFile(user.Id, stream, file.FileName);

                _logger.LogInformation("{Controller}.UploadImage({FirebaseId}) - After",
                    GetType().Name, firebaseId);
                return Ok(new { imageUrl = url });
            }

            _logger.LogWarning("{Controller}.UploadImage({FirebaseId}) - User not found",
                GetType().Name, firebaseId);
            return Unauthorized();
        }

        [HttpPost("upload-images")]
        public async Task<IActionResult> UploadImages(List<IFormFile> files, [FromHeader] string firebaseId)
        {
            _logger.LogInformation("{Controller}.UploadImages({FirebaseId}) - Before",
                GetType().Name, firebaseId);

            var user = await _userService.GetByFireBaseIdAsync(firebaseId);
            if (user != null)
            {
                var fileData = files.Select(f => (f.OpenReadStream(), f.FileName)).ToList();
                var urls = await _imageUploader.UploadFiles(user.Id, fileData);

                _logger.LogInformation("{Controller}.UploadImages({FirebaseId}) - After",
                    GetType().Name, firebaseId);
                return Ok(new { imageUrls = urls });
            }

            _logger.LogWarning("{Controller}.UploadImages({FirebaseId}) - User not found",
                GetType().Name, firebaseId);
            return Unauthorized();
        }
    }
}
