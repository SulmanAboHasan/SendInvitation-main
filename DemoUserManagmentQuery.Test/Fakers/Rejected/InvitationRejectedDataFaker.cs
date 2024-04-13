using DemoUserManagementQuerySide.EventsHandler.Rejected;
using DemoUserManagementQuerySide.EventsHandler.Sent;

namespace DemoUserManagmentQuery.Test.Fakers.Rejected
{
    public class InvitationRejectedDataFaker : RecordFaker<InvitationRejectedData>
    {
        public InvitationRejectedDataFaker(InvitationSent invitationSent)
        {
            RuleFor(e => e.Id, invitationSent.AggregateId);
            RuleFor(e => e.AccountId, faker => faker.Random.Guid().ToString());
            RuleFor(e => e.SubscriptionId, invitationSent.Data.SubscriptionId);
            RuleFor(e => e.MemberId, invitationSent.Data.MemberId);
        }
    }
}