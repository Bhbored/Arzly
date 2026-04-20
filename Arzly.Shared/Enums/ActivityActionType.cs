using System;
using System.Collections.Generic;
using System.Text;

namespace Arzly.Shared.Enums
{
    public enum ActivityActionType
    {
        Login,
        Logout,
        Register,
        PasswordChanged,

        ProfileUpdated,
        AccountDeleted,

        ProductCreated,
        ProductUpdated,
        ProductDeleted,

        OrderPlaced,
        OrderCancelled,
        PaymentCompleted,

        UserBanned,
        UserUnbanned,
        TicketCreated,
        TicketClosed
    }
}
