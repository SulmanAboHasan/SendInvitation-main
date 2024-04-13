namespace DemoUsersManagementCommandSide.Events.DataType
{
    public record MemberRemovedData(
        string AccountId,
        string SubscriptionId,
        string MemberId        
        );
    
}
