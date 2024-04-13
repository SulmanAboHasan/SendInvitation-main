namespace DemoUsersManagementCommandSide.Events.DataType
{
    public record InvitationAcceptedtData
    (
        string AccountId,
        string SubscriptionId,
        string MemberId
    );
}
