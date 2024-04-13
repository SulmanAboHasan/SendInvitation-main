namespace DemoUserManagementQuerySide.EventsHandler.Left
{
    public record MemberLeftData(
       string Id,
       string AccountId,
       string SubscriptionId,
       string MemberId);
}
