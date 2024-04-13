using DemoUsersManagementCommandSide.Events.DataType;

namespace DemoUsersManagementCommandSide.Events
{
    public record InvitationAccepted
    (
        string aggregateId,
        int sequence,
        DateTime dateTime,
        InvitationAcceptedtData data,
        string userId,
        int version
    ): Event<InvitationAcceptedtData>(aggregateId, sequence, dateTime, data, userId, version);
}
