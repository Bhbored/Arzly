using Arzly.Api.Filters.ResultFilters;
using Arzly.Api.Infrastructure.Storage;
using Arzly.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arzly.Api.Controllers.v1.Upload
{
    [JsonFormatter(UsePascalCase = true)]

    public class UploadController : CustomeControllerBase
    {
        private readonly ImageUploader _imageUploader;
        private readonly ILogger<UploadController> _logger;

        public UploadController(ImageUploader imageUploader, ILogger<UploadController> logger)
        {
            _imageUploader = imageUploader;
            _logger = logger;
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            _logger.LogInformation("{Controller}.UploadImage - Before",
                GetType().Name);

            using var stream = file.OpenReadStream();
            var url = await _imageUploader.UploadFile(User.GetUserId().ToString(), stream, file.FileName);

            _logger.LogInformation("{Controller}.UploadImage - After",
                GetType().Name);
            return Ok(new { imageUrl = url });
        }

        [HttpPost("upload-images")]
        public async Task<IActionResult> UploadImages(List<IFormFile> files)
        {
            _logger.LogInformation("{Controller}.UploadImages - Before",
                GetType().Name);

            var fileData = files.Select(f => (f.OpenReadStream(), f.FileName)).ToList();
            var urls = await _imageUploader.UploadFiles(User.GetUserId().ToString(), fileData);

            _logger.LogInformation("{Controller}.UploadImages - After",
                GetType().Name);
            return Ok(new { imageUrls = urls });
        }
    }
}
