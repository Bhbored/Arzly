namespace Arzly.Shared.DTOs.Response.Admin;

public class OperationalStatisticsResponse
{
    public int Users { get; set; }
    public int BannedUsers { get; set; }
    public int PendingListings { get; set; }
    public int ActiveListings { get; set; }
    public int RejectedListings { get; set; }
    public int DeletedListings { get; set; }
    public int UnresolvedReports { get; set; }
    public int OpenTickets { get; set; }
    public int UnreadNotifications { get; set; }
    public DateTime GeneratedAt { get; set; }
}
