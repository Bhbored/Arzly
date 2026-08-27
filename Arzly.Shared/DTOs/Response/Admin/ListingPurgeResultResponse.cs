namespace Arzly.Shared.DTOs.Response.Admin;

public sealed class ListingPurgeResultResponse
{
    public DateTime CutoffUtc { get; set; }
    public int PurgedListings { get; set; }
    public int DeletedImages { get; set; }
    public int FailedImageDeletions { get; set; }
}
