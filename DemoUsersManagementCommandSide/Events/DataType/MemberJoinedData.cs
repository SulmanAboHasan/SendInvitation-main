using DemoUsersManagementCommandSide.Domain;

namespace DemoUsersManagementCommandSide.Events.DataType
{
    public record MemberJoinedData(
        string AccountId,
        string SubscriptionId,
        string MemberId,
        Permission Permissions

        );
    
}
