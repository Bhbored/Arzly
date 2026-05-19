using Arzly.Api.Domain.Entities;
using Arzly.Api.Domain.ListingOwned;

namespace Arzly.Api.Helpers.ListingFilters
{
    public static class ServicesDetailsFilter
    {
        public static IQueryable<Listing> Apply(IQueryable<Listing> query, ServicesDetails d)
        {
            if (d.ServiceType.HasValue)
                query = query.Where(x => x.ServicesDetails!.ServiceType == d.ServiceType);
            if (d.HomeServiceType.HasValue)
                query = query.Where(x => x.ServicesDetails!.HomeServiceType == d.HomeServiceType);
            if (d.PersonalServiceType.HasValue)
                query = query.Where(x => x.ServicesDetails!.PersonalServiceType == d.PersonalServiceType);
            if (d.ProfessionalServiceType.HasValue)
                query = query.Where(x => x.ServicesDetails!.ProfessionalServiceType == d.ProfessionalServiceType);
            if (d.EventServiceType.HasValue)
                query = query.Where(x => x.ServicesDetails!.EventServiceType == d.EventServiceType);
            if (d.TransportServiceType.HasValue)
                query = query.Where(x => x.ServicesDetails!.TransportServiceType == d.TransportServiceType);
            if (d.OtherServiceType.HasValue)
                query = query.Where(x => x.ServicesDetails!.OtherServiceType == d.OtherServiceType);

            return query;
        }
    }
}
