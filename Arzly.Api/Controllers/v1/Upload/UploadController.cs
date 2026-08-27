using Arzly.Api.Filters.ResultFilters;
using Arzly.Api.Infrastructure.Storage;
using Arzly.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Arzly.Api.Controllers.v1.Upload
{
    [JsonFormatter(UsePascalCase = true)]

    public class UploadController : CustomeControllerBase
    {
        private const long MaximumFileSize = 10 * 1024 * 1024;
        private const long MaximumRequestSize = 50 * 1024 * 1024;
        private const int MaximumFileCount = 10;
        private static readonly HashSet<string> AllowedContentTypes = new(StringComparer.OrdinalIgnoreCase)
        {
            "image/jpeg", "image/png", "image/webp"
        };
        private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".webp"
        };
        private readonly IImageUploader _imageUploader;
        private readonly ILogger<UploadController> _logger;

        public UploadController(IImageUploader imageUploader, ILogger<UploadController> logger)
        {
            _imageUploader = imageUploader;
            _logger = logger;
        }

        [HttpPost("upload-image")]
        [EnableRateLimiting("uploads")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            _logger.LogInformation("{Controller}.UploadImage - Before",
                GetType().Name);

            var validationError = await ValidateFileAsync(file);
            if (validationError is not null)
                return BadRequest(validationError);

            using var stream = file.OpenReadStream();
            var url = await _imageUploader.UploadFile(
                User.GetUserId().ToString(), stream, file.FileName, HttpContext.RequestAborted);

            _logger.LogInformation("{Controller}.UploadImage - After",
                GetType().Name);
            return Ok(new { imageUrl = url });
        }

        [HttpPost("upload-images")]
        [EnableRateLimiting("uploads")]
        public async Task<IActionResult> UploadImages(List<IFormFile> files)
        {
            _logger.LogInformation("{Controller}.UploadImages - Before",
                GetType().Name);

            if (files.Count is 0 or > MaximumFileCount)
                return BadRequest($"Upload between 1 and {MaximumFileCount} images");
            if (files.Sum(file => file.Length) > MaximumRequestSize)
                return BadRequest($"The combined upload must be {MaximumRequestSize / 1024 / 1024} MB or smaller");

            foreach (var file in files)
            {
                var validationError = await ValidateFileAsync(file);
                if (validationError is not null)
                    return BadRequest(validationError);
            }

            var fileData = files.Select(f => (f.OpenReadStream(), f.FileName)).ToList();
            List<string> urls;
            try
            {
                urls = await _imageUploader.UploadFiles(
                    User.GetUserId().ToString(), fileData, HttpContext.RequestAborted);
            }
            finally
            {
                foreach (var (stream, _) in fileData)
                    stream.Dispose();
            }

            _logger.LogInformation("{Controller}.UploadImages - After",
                GetType().Name);
            return Ok(new { imageUrls = urls });
        }

        [HttpDelete("uploaded-image")]
        [EnableRateLimiting("uploads")]
        public async Task<IActionResult> DeleteUploadedImage([FromQuery] string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return BadRequest("An image URL is required");

            var deleted = await _imageUploader.DeleteFile(
                User.GetUserId().ToString(), url, HttpContext.RequestAborted);
            return deleted ? NoContent() : NotFound();
        }

        private static async Task<string?> ValidateFileAsync(IFormFile? file)
        {
            if (file is null || file.Length == 0)
                return "An image file is required";
            if (file.Length > MaximumFileSize)
                return $"Each image must be {MaximumFileSize / 1024 / 1024} MB or smaller";

            var extension = Path.GetExtension(file.FileName);
            if (!AllowedContentTypes.Contains(file.ContentType) || !AllowedExtensions.Contains(extension))
                return "Only JPEG, PNG, and WebP images are allowed";

            await using var stream = file.OpenReadStream();
            var header = new byte[12];
            var bytesRead = 0;
            while (bytesRead < header.Length)
            {
                var read = await stream.ReadAsync(header.AsMemory(bytesRead, header.Length - bytesRead));
                if (read == 0)
                    break;
                bytesRead += read;
            }

            if (!HasMatchingSignature(file.ContentType, header, bytesRead))
                return "The file contents do not match the declared image type";

            return null;
        }

        private static bool HasMatchingSignature(string contentType, byte[] header, int bytesRead) =>
            contentType.ToLowerInvariant() switch
            {
                "image/jpeg" => bytesRead >= 3 && header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF,
                "image/png" => bytesRead >= 8 && header.AsSpan(0, 8).SequenceEqual(
                    new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }),
                "image/webp" => bytesRead >= 12 &&
                    header.AsSpan(0, 4).SequenceEqual("RIFF"u8) &&
                    header.AsSpan(8, 4).SequenceEqual("WEBP"u8),
                _ => false
            };
    }
}
