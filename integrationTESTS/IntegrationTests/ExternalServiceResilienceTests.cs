using System.Net;
using System.Text;
using Arzly.Api.Helpers.GoogleMap;
using Arzly.Api.Infrastructure.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;

namespace Arzly.IntegrationTests;

public class ExternalServiceResilienceTests
{
    [Fact]
    public async Task UploadFiles_RollsBackEarlierObjectsWhenALaterUploadFails()
    {
        var storage = new RecordingObjectStorage(failOnPutNumber: 2);
        var uploader = CreateUploader(storage);
        var files = new List<(Stream Stream, string FileName)>
        {
            (new MemoryStream([1]), "first.png"),
            (new MemoryStream([2]), "second.png")
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            uploader.UploadFiles("user-1", files));

        Assert.Single(storage.SuccessfulKeys);
        Assert.Equal(storage.SuccessfulKeys, storage.DeletedKeys);
    }

    [Fact]
    public async Task DeleteFile_DeletesOnlyAValidObjectOwnedByTheUser()
    {
        var storage = new RecordingObjectStorage(failOnPutNumber: int.MaxValue);
        var uploader = CreateUploader(storage);
        var objectId = Guid.NewGuid();

        var owned = await uploader.DeleteFile(
            "user-1", $"https://uploads.arzly.test/user-1/{objectId:N}.png");
        var foreign = await uploader.DeleteFile(
            "user-1", $"https://uploads.arzly.test/user-2/{Guid.NewGuid():N}.png");

        Assert.True(owned);
        Assert.False(foreign);
        Assert.Equal($"user-1/{objectId:N}.png", Assert.Single(storage.DeletedKeys));
    }

    [Fact]
    public async Task GoogleMaps_RetriesTransientResponsesAndReturnsSuccessfulResult()
    {
        var handler = new SequenceHandler(
            new HttpResponseMessage(HttpStatusCode.ServiceUnavailable),
            JsonResponse("{\"suggestions\":[]}"));
        var service = CreateGoogleMapsService(handler);

        var result = await service.AutocompleteAsync("Beirut");

        Assert.Empty(result);
        Assert.Equal(2, handler.RequestCount);
    }

    [Fact]
    public async Task GoogleMaps_DoesNotRetryPermanentClientErrors()
    {
        var handler = new SequenceHandler(new HttpResponseMessage(HttpStatusCode.BadRequest));
        var service = CreateGoogleMapsService(handler);

        await Assert.ThrowsAsync<HttpRequestException>(() => service.AutocompleteAsync("Beirut"));

        Assert.Equal(1, handler.RequestCount);
    }

    private static ImageUploader CreateUploader(IR2ObjectStorage storage)
    {
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["CloudflareR2:BucketName"] = "test-bucket",
            ["CloudflareR2:PublicUrlBase"] = "https://uploads.arzly.test"
        }).Build();
        return new ImageUploader(NullLogger<ImageUploader>.Instance, configuration, storage);
    }

    private static GoogleMapsService CreateGoogleMapsService(HttpMessageHandler handler)
    {
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["GoogleMaps:ApiKey"] = "test-key"
        }).Build();
        return new GoogleMapsService(
            new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(2) },
            configuration,
            NullLogger<GoogleMapsService>.Instance);
    }

    private static HttpResponseMessage JsonResponse(string json) => new(HttpStatusCode.OK)
    {
        Content = new StringContent(json, Encoding.UTF8, "application/json")
    };

    private sealed class RecordingObjectStorage(int failOnPutNumber) : IR2ObjectStorage
    {
        private int _putCount;
        public List<string> SuccessfulKeys { get; } = [];
        public List<string> DeletedKeys { get; } = [];

        public Task PutAsync(string bucketName, string objectKey, Stream stream, CancellationToken cancellationToken)
        {
            _putCount++;
            if (_putCount == failOnPutNumber)
                throw new IOException("Simulated storage failure");
            SuccessfulKeys.Add(objectKey);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(string bucketName, string objectKey, CancellationToken cancellationToken)
        {
            DeletedKeys.Add(objectKey);
            return Task.CompletedTask;
        }
    }

    private sealed class SequenceHandler(params HttpResponseMessage[] responses) : HttpMessageHandler
    {
        private readonly Queue<HttpResponseMessage> _responses = new(responses);
        public int RequestCount { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            RequestCount++;
            return Task.FromResult(_responses.Dequeue());
        }
    }
}
