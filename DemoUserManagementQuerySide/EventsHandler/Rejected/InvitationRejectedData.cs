namespace DemoUserManagementQuerySide.EventsHandler.Rejected
{
    public record InvitationRejectedData(
      string Id,
      string AccountId,
      string SubscriptionId,
      string MemberId
       );
}
