namespace DemoUsersManagementCommandSide.Events.DataType
{
    public record MemberLeftData(
        string AccountId,
        string SubscriptionId,
        string MemberId
        );
    

}
