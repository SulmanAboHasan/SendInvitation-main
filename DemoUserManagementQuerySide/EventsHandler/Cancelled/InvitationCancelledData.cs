namespace DemoUserManagementQuerySide.EventsHandler.Cancelled
{
    public record InvitationCancelledData(
       string Id,
       string AccountId,
       string SubscriptionId,
       string MemberId
        );
}
