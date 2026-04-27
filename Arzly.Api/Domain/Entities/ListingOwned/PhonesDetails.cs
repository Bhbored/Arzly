using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums.ListingOwned.PhonesAndGadgets;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arzly.Api.Domain.ListingOwned
{
    public class PhonesDetails
    {
        [Key, ForeignKey(nameof(Listing))]
        public Guid ListingId { get; set; }
        public virtual Listing? Listing { get; set; }

        public PhoneBrand? PhoneBrand { get; set; }
        public PhoneCondition? PhoneCondition { get; set; }
        public PhoneStorage? Storage { get; set; }
        public PhoneColor? Color { get; set; }

        public AccessoryBrand? AccessoryBrand { get; set; }
        public MobileAccessoryType? AccessoryItemType { get; set; }

        public MobileOperator? Operator { get; set; }
        public MembershipType? MembershipType { get; set; }
        public WatchBrand? WatchBrand { get; set; }
        public WatchStorage? WatchStorage { get; set; }
        public BandMaterial? BandMaterial { get; set; }
        public BandColor? BandColor { get; set; }
    }
}
