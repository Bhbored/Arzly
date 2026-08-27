using Arzly.Shared.Constants;
using SerilogTimings;

namespace Arzly.Api.Infrastructure.Storage;

public sealed class ImageUploader : IImageUploader
{
    private readonly ILogger<ImageUploader> _logger;
    private readonly IR2ObjectStorage _storage;
    private readonly string _bucketName;
    private readonly string _publicUrlBase;

    public ImageUploader(
        ILogger<ImageUploader> logger,
        IConfiguration configuration,
        IR2ObjectStorage storage)
    {
        _logger = logger;
        _storage = storage;
        _bucketName = configuration["CloudflareR2:BucketName"]!;
        _publicUrlBase = configuration["CloudflareR2:PublicUrlBase"]!.TrimEnd('/');
    }

    public async Task<string> UploadFile(
        string userId,
        Stream fileStream,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        var (_, url) = await UploadObjectAsync(userId, fileStream, fileName, cancellationToken);
        return url;
    }

    public async Task<List<string>> UploadFiles(
        string userId,
        List<(Stream Stream, string FileName)> files,
        CancellationToken cancellationToken = default)
    {
        var uploaded = new List<(string Key, string Url)>();
        try
        {
            using (Operation.Time("Time for uploading images to Cloudflare R2"))
            {
                foreach (var (stream, fileName) in files)
                    uploaded.Add(await UploadObjectAsync(userId, stream, fileName, cancellationToken));
            }

            return uploaded.Select(item => item.Url).ToList();
        }
        catch
        {
            await RollBackAsync(uploaded.Select(item => item.Key));
            throw;
        }
    }

    public bool IsOwnedFileUrl(string userId, string fileUrl) =>
        TryGetOwnedObjectKey(userId, fileUrl, out _);

    public async Task<bool> DeleteFile(
        string userId,
        string fileUrl,
        CancellationToken cancellationToken = default)
    {
        if (!TryGetOwnedObjectKey(userId, fileUrl, out var objectKey))
            return false;

        await _storage.DeleteAsync(_bucketName, objectKey, cancellationToken);
        _logger.LogInformation("Deleted owned object. Key: {ObjectKey}", objectKey);
        return true;
    }

    private async Task<(string Key, string Url)> UploadObjectAsync(
        string userId,
        Stream fileStream,
        string fileName,
        CancellationToken cancellationToken)
    {
        var objectKey = $"{userId}/{Guid.NewGuid():N}{Path.GetExtension(fileName).ToLowerInvariant()}";
        try
        {
            await _storage.PutAsync(_bucketName, objectKey, fileStream, cancellationToken);
            _logger.LogInformation("File uploaded to object storage. Key: {ObjectKey}", objectKey);
            return (objectKey, $"{_publicUrlBase}/{objectKey}");
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to upload file to object storage. Key: {ObjectKey}", objectKey);
            throw new InvalidOperationException(ExceptionMessages.FailedUpload, exception);
        }
    }

    private async Task RollBackAsync(IEnumerable<string> objectKeys)
    {
        foreach (var objectKey in objectKeys.Reverse())
        {
            try
            {
                await _storage.DeleteAsync(_bucketName, objectKey, CancellationToken.None);
                _logger.LogInformation("Rolled back uploaded object. Key: {ObjectKey}", objectKey);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to roll back uploaded object. Key: {ObjectKey}", objectKey);
            }
        }
    }

    private bool TryGetOwnedObjectKey(string userId, string fileUrl, out string objectKey)
    {
        objectKey = string.Empty;
        if (!Uri.TryCreate(_publicUrlBase, UriKind.Absolute, out var publicBase) ||
            !Uri.TryCreate(fileUrl, UriKind.Absolute, out var candidate) ||
            !string.Equals(publicBase.Scheme, candidate.Scheme, StringComparison.OrdinalIgnoreCase) ||
            !string.Equals(publicBase.Host, candidate.Host, StringComparison.OrdinalIgnoreCase) ||
            publicBase.Port != candidate.Port || !string.IsNullOrEmpty(candidate.Query) ||
            !string.IsNullOrEmpty(candidate.Fragment))
            return false;

        var basePath = publicBase.AbsolutePath.TrimEnd('/');
        var relativePath = candidate.AbsolutePath;
        if (basePath.Length > 0)
        {
            if (!relativePath.StartsWith($"{basePath}/", StringComparison.Ordinal))
                return false;
            relativePath = relativePath[(basePath.Length + 1)..];
        }
        else
        {
            relativePath = relativePath.TrimStart('/');
        }

        var segments = relativePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (segments.Length != 2 || segments[0] != userId)
            return false;

        var fileName = segments[1];
        var extension = Path.GetExtension(fileName);
        var identifier = Path.GetFileNameWithoutExtension(fileName);
        if (!Guid.TryParse(identifier, out _) || extension is not (".jpg" or ".jpeg" or ".png" or ".webp"))
            return false;

        objectKey = $"{userId}/{fileName}";
        return true;
    }
}
