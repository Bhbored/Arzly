using Arzly.Api.Application.Contracts.Admin;
using Arzly.Api.Infrastructure.Data.DataBaseContext;
using Arzly.Shared.DTOs.Response.Admin;
using Arzly.Shared.Enums.Listing;
using Arzly.Shared.Enums.Ticket;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Application.Services.Admin;

public class AdminStatisticsService : IAdminStatisticsService
{
    private readonly AppDbContext _db;

    public AdminStatisticsService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<OperationalStatisticsResponse> GetAsync()
    {
        var listings = _db.Listings.IgnoreQueryFilters();
        return new OperationalStatisticsResponse
        {
            Users = await _db.Users.CountAsync(),
            BannedUsers = await _db.Users.CountAsync(x => x.IsBanned),
            PendingListings = await listings.CountAsync(x => !x.IsDeleted && x.Status == ListingStatus.Pending),
            ActiveListings = await listings.CountAsync(x => !x.IsDeleted && x.Status == ListingStatus.Active),
            RejectedListings = await listings.CountAsync(x => !x.IsDeleted && x.Status == ListingStatus.Rejected),
            DeletedListings = await listings.CountAsync(x => x.IsDeleted),
            UnresolvedReports = await _db.UserReports.CountAsync(x => !x.IsResolved),
            OpenTickets = await _db.Tickets.CountAsync(x =>
                x.Status == TicketStatus.Open || x.Status == TicketStatus.InProgress),
            UnreadNotifications = await _db.Notifications.CountAsync(x => !x.IsRead),
            GeneratedAt = DateTime.UtcNow
        };
    }
}
