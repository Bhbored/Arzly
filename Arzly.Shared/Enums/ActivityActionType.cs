using System;
using System.Collections.Generic;
using System.Text;

namespace Arzly.Shared.Enums
{
    public enum ActivityActionType
    {
        // Auth
        Login,
        Logout,
        Register,
        PasswordChanged,
        PasswordReset,

        // Profile
        ProfileUpdated,
        ProfilePictureUpdated,
        AccountDeleted,

        // Listings
        ListingCreated,
        ListingUpdated,
        ListingDeleted,
        ListingViewed,
        ListingShared,

        // Saved Listings
        ListingSaved,
        ListingUnsaved,

        // Chat
        ChatStarted,
        MessageSent,
        MessageRead,
        ChatArchived,

        // Reports
        ReportSubmitted,
        ReportResolved,
        ReportDismissed,

        // Tickets (Support)
        TicketCreated,
        TicketMessageSent,
        TicketClosed,
        TicketReopened,

        // Admin Actions
        UserBanned,
        UserUnbanned,
        ListingFlagged,
        ListingUnflagged
    }
}
