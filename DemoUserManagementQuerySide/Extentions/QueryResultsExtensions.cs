using DemoUserManagementQuerySide.Entities;
using System.Runtime.CompilerServices;

namespace DemoUserManagementQuerySide.Extentions
{
    public static class QueryResultsExtensions
    {
        public static SubscriberOutput ToSubscriberOutput(this Subscriper subscriper)
            => new()
            {
                Id = subscriper.Id,
                SubscriptionId = subscriper.SubscriptionId,
                SubscriptionDescription = subscriper.Subscription?.Description,
                UserId = subscriper.UserId,
                UserName = subscriper.User?.Name,
                Status = subscriper.Status,
                JoinedAt = subscriper.JoinedAt.ToUtcTimestamp(),
            };

        public static InvitationOutput ToInvitationOutput(this Invitation invitation)
            => new()
            {
                Id = invitation.Id,
                SubscriptionId = invitation.SubscriptionId,
                SubscriptionDescription = invitation.Subscriptions.Description,
                MemberId = invitation.UserId,
                MemberName = invitation.Users?.Name,
                Status = invitation.Status,
                SentAt = invitation.SentAt.ToUtcTimestamp()
            };
    }
}
