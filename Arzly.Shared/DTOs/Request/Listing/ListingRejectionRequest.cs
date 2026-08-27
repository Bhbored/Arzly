using System.ComponentModel.DataAnnotations;

namespace Arzly.Shared.DTOs.Request.Listing;

public class ListingRejectionRequest
{
    [Required, MaxLength(1000)]
    public string Reason { get; set; } = string.Empty;
}
