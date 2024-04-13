using DemoUserManagementQuerySide.Entities;

namespace DemoUserManagementQuerySide.EventsHandler.Joined
{
    public record MemberJoinedData(
    string AccountId,
    string SubscriptionId,
    string MemberId,
    Permission Permissions
    );
}
