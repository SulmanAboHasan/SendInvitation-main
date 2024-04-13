using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagementQuerySide.Entities;
using DemoUserManagementQuerySide.EventsHandler.Sent;
using MediatR;

namespace DemoUserManagementQuerySide.EventsHandler.Removed
{
    public record class MemberRemoved
    (
        string AggregateId,
        int Sequence,
        MemberRemovedData Data,
        DateTime DateTime,
        string UserId,
        int Version
        ) : Event<MemberRemovedData>(
            AggregateId: AggregateId,
            Sequence: Sequence,
            Data: Data,
            DateTime: DateTime,
            UserId: UserId,
            Version: Version
            );

   
}
