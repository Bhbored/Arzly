using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Domain.Entities
{
    [Owned]
    public class Coordinate
    {
        public double lat;
        public double lon;
    }
}
