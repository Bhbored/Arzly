using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;

namespace Arzly.Api.Infrastructure.Storage;

public sealed class R2ObjectStorage : IR2ObjectStorage, IDisposable
{
    private readonly IAmazonS3 _client;

    public R2ObjectStorage(IConfiguration configuration)
    {
        var credentials = new BasicAWSCredentials(
            configuration["CloudflareR2:AccessKey"],
            configuration["CloudflareR2:SecretKey"]);
        _client = new AmazonS3Client(credentials, new AmazonS3Config
        {
            ServiceURL = configuration["CloudflareR2:ServiceURL"],
            ForcePathStyle = true,
            Timeout = TimeSpan.FromSeconds(30)
        });
    }

    public async Task PutAsync(
        string bucketName,
        string objectKey,
        Stream stream,
        CancellationToken cancellationToken)
    {
        await _client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = bucketName,
            Key = objectKey,
            InputStream = stream,
            DisablePayloadSigning = true
        }, cancellationToken);
    }

    public Task DeleteAsync(
        string bucketName,
        string objectKey,
        CancellationToken cancellationToken) =>
        _client.DeleteObjectAsync(new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = objectKey
        }, cancellationToken);

    public void Dispose() => _client.Dispose();
}
