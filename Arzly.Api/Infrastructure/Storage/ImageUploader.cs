using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Arzly.Shared.Constants;
using SerilogTimings;

namespace Arzly.Api.Infrastructure.Storage;

public class ImageUploader : IDisposable
{
    private readonly ILogger<ImageUploader> _logger;
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;
    private readonly string _publicUrlBase;

    public ImageUploader(ILogger<ImageUploader> logger, IConfiguration configuration)
    {
        _logger = logger;

        var accessKey = configuration["CloudflareR2:AccessKey"]!;
        var secretKey = configuration["CloudflareR2:SecretKey"]!;
        var serviceUrl = configuration["CloudflareR2:ServiceURL"]!;
        _bucketName = configuration["CloudflareR2:BucketName"]!;
        _publicUrlBase = configuration["CloudflareR2:PublicUrlBase"]!;

        var credentials = new BasicAWSCredentials(accessKey, secretKey);

        _s3Client = new AmazonS3Client(credentials, new AmazonS3Config
        {
            ServiceURL = serviceUrl,
            ForcePathStyle = true,
        });

        _logger.LogInformation("Cloudflare R2 client initialized");
    }

    public async Task<string> UploadFile(string userId, Stream fileStream, string fileName)
    {
        try
        {
            var objectKey = $"{userId}/{Guid.NewGuid()}{Path.GetExtension(fileName)}";

            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = objectKey,
                InputStream = fileStream,
                DisablePayloadSigning = true,
            };

            var response = await _s3Client.PutObjectAsync(request);
            _logger.LogInformation("File uploaded. Key: {Key}, ETag: {ETag}", objectKey, response.ETag);

            var fileUrl = $"{_publicUrlBase}/{objectKey}";
            _logger.LogInformation("File URL: {Url}", fileUrl);

            return fileUrl;
        }
        catch (Exception e)
        {
            _logger.LogInformation("Failed to Upload the Image");
            throw new InvalidOperationException(ExceptionMessages.FailedUpload, e);
        }

    }

    public async Task<List<string>> UploadFiles(string userId, List<(Stream Stream, string FileName)> files)
    {

        var urls = new List<string>();

        using (Operation.Time("Time for Upload Images To cloudflare and providing valid urls"))
        {
            foreach (var (stream, fileName) in files)
            {
                var url = await UploadFile(userId, stream, fileName);
                urls.Add(url);
            }
        }
        return urls;

    }

    public void Dispose()
    {
        _s3Client?.Dispose();
    }
}