using DemoUsersManagementCommandSide.Domain;

namespace DemoUsersManagementCommandSide.Events.DataType
{
    public record InvitationSentData
    (
        string AccountId,
        string SubscriptionId,
        string MemberId,
        Domain.Permission Permissions
    );
}
