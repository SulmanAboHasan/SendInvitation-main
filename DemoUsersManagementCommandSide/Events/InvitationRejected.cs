using DemoUsersManagementCommandSide.Events.DataType;

namespace DemoUsersManagementCommandSide.Events
{
    public record InvitationRejected
    (
        string aggregateId,
        int sequence,
        DateTime dateTime,
        InvitationRejectedtData data,
        string userId,
        int version
    ): Event<InvitationRejectedtData>(aggregateId, sequence, dateTime, data, userId, version);


}
