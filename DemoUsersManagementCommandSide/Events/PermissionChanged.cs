using DemoUsersManagementCommandSide.Events.DataType;

namespace DemoUsersManagementCommandSide.Events
{
    public record PermissionChanged(
        string aggregateId,
        int sequence,
        DateTime dateTime,
        PermissionChangedData data,
        string userId,
        int version
        ): Event<PermissionChangedData>(aggregateId, sequence, dateTime, data, userId, version);
}
