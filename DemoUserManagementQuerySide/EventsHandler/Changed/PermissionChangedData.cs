using DemoUserManagementQuerySide.Entities;

namespace DemoUserManagementQuerySide.EventsHandler.Changed
{
    public record PermissionChangedData(
        string Id,
        string AccountId,
        string SubscriptionId,
        string MemberId,
        Permission Permissions
        );
}
