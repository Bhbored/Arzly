namespace Arzly.Api.Infrastructure.Storage;

public interface IImageUploader
{
    Task<string> UploadFile(string userId, Stream fileStream, string fileName, CancellationToken cancellationToken = default);
    Task<List<string>> UploadFiles(string userId, List<(Stream Stream, string FileName)> files, CancellationToken cancellationToken = default);
    Task<bool> DeleteFile(string userId, string fileUrl, CancellationToken cancellationToken = default);
    bool IsOwnedFileUrl(string userId, string fileUrl);
}
