namespace DemoUsersManagementCommandSide.Events.DataType
{
    public record InvitationCanceledData
    (
        string AccountId,
        string SubscriptionId,
        string MemberId
    );
}
