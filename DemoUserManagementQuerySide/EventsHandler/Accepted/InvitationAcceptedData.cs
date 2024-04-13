namespace DemoUserManagementQuerySide.EventsHandler.Accepted
{
    public record InvitationAcceptedData(
       string Id,
       string AccountId,
       string SubscriptionId,
       string MemberId
        );
}
