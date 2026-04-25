using Arzly.Shared.Enums.PhonesDetails;

namespace Arzly.Api.Domain.Entities.ListingOwned
{
    public class PhonesDetails
    {

        public PhoneBrand? PhoneBrand { get; set; }
        public PhoneCondition? PhoneCondition { get; set; }
        public PhoneStorage? Storage { get; set; }
        public PhoneColor? Color { get; set; }

        public AccessoryBrand? AccessoryBrand { get; set; }
        public MobileAccessoryType? AccessoryItemType { get; set; }
        

        public MobileOperator? Operator { get; set; }
        public MembershipType? MembershipType { get; set; }


        public WatchBrand? WatchBrand { get; set; }
        // Reuses PhoneCondition
        public WatchStorage? WatchStorage { get; set; }
        public BandMaterial? BandMaterial { get; set; }
        public BandColor? BandColor { get; set; }
    }
}
