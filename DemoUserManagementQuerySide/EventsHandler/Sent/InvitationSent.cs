using DemoUserManagementQuerySide.EventsHandler.Sent;

namespace DemoUserManagementQuerySide.EventsHandler.Sent
{
    public record class InvitationSent(
        string AggregateId,
        int Sequence,
        InvitationSentData Data,
        DateTime DateTime,
        string UserId,
        int Version
        ) : Event<InvitationSentData>(
            AggregateId: AggregateId,
            Sequence: Sequence,
            Data: Data,
            DateTime: DateTime,
            UserId: UserId,
            Version: Version
            );

}
