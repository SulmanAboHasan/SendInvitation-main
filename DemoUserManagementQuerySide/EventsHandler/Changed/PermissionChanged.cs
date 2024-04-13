namespace DemoUserManagementQuerySide.EventsHandler.Changed
{
    public record class PermissionChanged(
        string AggregateId,
        int Sequence,
        PermissionChangedData Data,
        DateTime DateTime,
        string UserId,
        int Version
        ) : Event<PermissionChangedData>(
            AggregateId: AggregateId,
            Sequence: Sequence,
            Data: Data,
            DateTime: DateTime,
            UserId: UserId,
            Version: Version);
}
