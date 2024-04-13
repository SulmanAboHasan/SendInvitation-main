using DemoUserManagementQuerySide.Entities;

namespace DemoUserManagementQuerySide.EventsHandler.Sent
{
    public record InvitationSentData(
    string AccountId,
    string SubscriptionId,
    string MemberId,
    Permission Permissions
    );

}
