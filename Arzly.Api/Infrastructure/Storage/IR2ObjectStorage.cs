namespace Arzly.Api.Infrastructure.Storage;

public interface IR2ObjectStorage
{
    Task PutAsync(string bucketName, string objectKey, Stream stream, CancellationToken cancellationToken);
    Task DeleteAsync(string bucketName, string objectKey, CancellationToken cancellationToken);
}
