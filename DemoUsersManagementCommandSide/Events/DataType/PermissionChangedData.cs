using DemoUsersManagementCommandSide.Domain;

namespace DemoUsersManagementCommandSide.Events.DataType
{
    public record PermissionChangedData(
         string AccountId,
        string SubscriptionId,
        string MemberId,
        Permission Permissions
        );
    
}
