using DemoUserManagementQuerySide.QueryHandler.MemberInvitationPending;
using DemoUserManagementQuerySide.QueryHandler.MemberSubscription;
using DemoUserManagementQuerySide.QueryHandler.OwnerInvitationPending;
using DemoUserManagementQuerySide.QueryHandler.Subscriptions;

namespace DemoUserManagementQuerySide.Extentions
{
    public static class QueryRequestsExtensions
    {
        public static SubscriptionsQuery ToQuery(this GetSubscriptionsRequest request)
            => new(
                SubscriptionId: request.SubscriptionId,
                Page: request.Page ?? 1,
                Size: request.Size ?? 20
                );

        public static OwnerInvitationPendingQuery ToQuery(this GetOwnerInvitationPendingRequest request)
            => new(
                UserId: request.OwnerId,
                Page: request.Page ?? 1,
                Size: request.Size ?? 20
                );

        public static MemberInvitationPendingQuery ToQuery(this GetMemberInvitationPendingRequest request)
            => new(
                UserId: request.MemberId,
                Page: request.Page ?? 1,
                Size: request.Size ?? 20
                );

        public static MemberSubscriptionQuery ToQuery(this GetMemberSubscriptionRequest request)
            => new(
                UserId: request.MemberId,
                Page: request.Page ?? 1,
                Size: request.Size ?? 20
                );

    }
}
