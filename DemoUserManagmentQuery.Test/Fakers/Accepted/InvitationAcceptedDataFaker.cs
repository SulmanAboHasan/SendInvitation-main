using DemoUserManagementQuerySide.EventsHandler.Accepted;
using DemoUserManagementQuerySide.EventsHandler.Sent;

namespace DemoUserManagmentQuery.Test.Fakers.Accepted
{
    public class InvitationAcceptedDataFaker : RecordFaker<InvitationAcceptedData>
    {
        public InvitationAcceptedDataFaker(InvitationSent invitationSent)
        {
            RuleFor(e => e.Id, invitationSent.AggregateId);
            RuleFor(e => e.AccountId, faker => faker.Random.Guid().ToString());
            RuleFor(e => e.SubscriptionId, invitationSent.Data.SubscriptionId);
            RuleFor(e => e.MemberId, invitationSent.Data.MemberId);
        }
    }
}