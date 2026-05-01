using Arzly.Shared.Enums.Listing;
using Arzly.Shared.Enums.JobListing;
using Arzly.Shared.DTOs.Response.PickupLocation;

namespace Arzly.Shared.DTOs.Response.Listing
{
    public class ListingResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public bool IsPriceNegotiable { get; set; }
        public ListingStatus Status { get; set; }
        public string? PrimaryImageUrl { get; set; }
        public List<string>? ImagesUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Owner
        public string OwnerId { get; set; } = string.Empty;

        public Guid CategoryId { get; set; }

        public Guid SubcategoryId { get; set; }

        public Guid PickupLocationId { get; set; }

        public PickupLocationResponse? PickupLocation { get; set; } 
        public object? ListingDetails { get; set; }

        // Contact
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public ContactMethod ContactMethod { get; set; }

        // Promotion
        public bool IsPromoted { get; set; }
        public PromotionType? PromotionType { get; set; }
        public DateTime? PromotionStartDate { get; set; }
        public DateTime? PromotionEndDate { get; set; }

        public override string ToString()
        {
            return $"ListingResponse [Id={Id}, Title={Title}, Description={Description}, Price={Price}, " +
                   $"IsPriceNegotiable={IsPriceNegotiable}, Status={Status}, PrimaryImageUrl={PrimaryImageUrl}, " +
                   $"ImagesCount={ImagesUrl?.Count ?? 0}, CreatedAt={CreatedAt}, UpdatedAt={UpdatedAt}, " +
                   $"OwnerId={OwnerId}, CategoryId={CategoryId}, SubcategoryId={SubcategoryId}, " +
                   $"PickupLocationId={PickupLocationId}, Name={Name}, PhoneNumber={PhoneNumber}, " +
                   $"ContactMethod={ContactMethod}, IsPromoted={IsPromoted}, PromotionType={PromotionType}, " +
                   $"PromotionStartDate={PromotionStartDate}, PromotionEndDate={PromotionEndDate}]";
        }


    }
}



