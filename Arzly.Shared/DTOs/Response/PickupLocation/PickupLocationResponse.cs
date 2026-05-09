using System;
using Arzly.Shared.Enums.Listing;

namespace Arzly.Shared.DTOs.Response.PickupLocation
{
    public class PickupLocationResponse
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public LocationLabel Label { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public bool IsDefault { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is PickupLocationResponse other)
            {
                return Id == other.Id
                    && UserId == other.UserId
                    && Label == other.Label
                    && Address == other.Address
                    && Notes == other.Notes
                    && Lat == other.Lat
                    && Lon == other.Lon
                    && IsDefault == other.IsDefault;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"PickupLocationResponse {{ Id={Id}, UserId={UserId}, Label={Label}, Address={Address}, Notes={Notes}, Lat={Lat}, Lon={Lon}, IsDefault={IsDefault} }}";
        }
    }
}
