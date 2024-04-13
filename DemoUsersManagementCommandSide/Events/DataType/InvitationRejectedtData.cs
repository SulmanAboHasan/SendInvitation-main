namespace DemoUsersManagementCommandSide.Events.DataType
{
    public record InvitationRejectedtData
    (
        string AccountId,
        string SubscriptionId,
        string MemberId
    );
}
