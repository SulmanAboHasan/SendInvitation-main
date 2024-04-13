using DemoUsersManagementCommandSide.Events.DataType;

namespace DemoUsersManagementCommandSide.Events
{
    public record MemberLeft(
     string aggregateId,
        int sequence,
        DateTime dateTime,
        MemberLeftData data,
        string userId,
        int version
        ): Event<MemberLeftData>(aggregateId, sequence, dateTime, data, userId, version);
}
