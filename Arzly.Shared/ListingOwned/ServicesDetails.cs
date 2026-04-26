using Arzly.Shared.Enums.ListingOwned.ServicesDetails;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Shared.ListingOwned
{
    [Owned]
    public class ServicesDetails
    {

        public ServiceType? ServiceType { get; set; } // Offering or Requesting

        public HomeServiceType? HomeServiceType { get; set; }


        public PersonalServiceType? PersonalServiceType { get; set; }

        public ProfessionalServiceType? ProfessionalServiceType { get; set; }


        public EventServiceType? EventServiceType { get; set; }

 
        public TransportServiceType? TransportServiceType { get; set; }

        public OtherServiceType? OtherServiceType { get; set; }

    }
}