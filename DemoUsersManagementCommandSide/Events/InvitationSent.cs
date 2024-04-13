using DemoUsersManagementCommandSide.Domain;
using DemoUsersManagementCommandSide.Events.DataType;

namespace DemoUsersManagementCommandSide.Events
{
    public record InvitationSent(
            string aggregateId,
            int sequence,
            DateTime dateTime,
            InvitationSentData Data,
            string userId,
            int version
        ): Event<InvitationSentData>(aggregateId, sequence, dateTime, Data, userId, version);
}
     
