using DemoUsersManagementCommandSide.Events.DataType;

namespace DemoUsersManagementCommandSide.Events
{
    public record InvitationCanceled
    (
        string aggregateId,
        int sequence,
        DateTime dateTime,        
        InvitationCanceledData data,
        string userId,
        int version
        ) : Event<InvitationCanceledData>(aggregateId, sequence, dateTime, data, userId, version);
       

        
    
}
