using Arzly.Api.Domain.Entities;
using Arzly.Shared.Enums.ListingOwned.ServicesDetails;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Arzly.Api.Domain.ListingOwned
{
    public class ServicesDetails
    {
        [Key, ForeignKey(nameof(Listing))]
        public Guid ListingId { get; set; }
        [JsonIgnore]
        public virtual Listing? Listing { get; set; }

        public ServiceType? ServiceType { get; set; }

        public HomeServiceType? HomeServiceType { get; set; }


        public PersonalServiceType? PersonalServiceType { get; set; }

        public ProfessionalServiceType? ProfessionalServiceType { get; set; }


        public EventServiceType? EventServiceType { get; set; }


        public TransportServiceType? TransportServiceType { get; set; }

        public OtherServiceType? OtherServiceType { get; set; }

    }
}
