using DemoUsersManagementCommandSide.Events.DataType;

namespace DemoUsersManagementCommandSide.Events
{
    public record MemberRemoved(
        string aggregateId,
        int sequence,
        DateTime dateTime,
        MemberRemovedData data,
        string userId,
        int version
        ): Event<MemberRemovedData>(aggregateId, sequence, dateTime, data, userId, version);
    
}
