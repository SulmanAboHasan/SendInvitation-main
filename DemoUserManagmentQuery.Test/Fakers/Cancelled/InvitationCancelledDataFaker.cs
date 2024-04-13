using DemoUserManagementQuerySide.EventsHandler.Cancelled;
using DemoUserManagementQuerySide.EventsHandler.Sent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagmentQuery.Test.Fakers.Cancelled
{
    public class InvitationCancelledDataFaker : RecordFaker<InvitationCancelledData>
    {
        public InvitationCancelledDataFaker(InvitationSent invitationSent)
        {
            RuleFor(e => e.Id, invitationSent.AggregateId);
            RuleFor(e => e.AccountId, faker => faker.Random.Guid().ToString());
            RuleFor(e => e.SubscriptionId, invitationSent.Data.SubscriptionId);
            RuleFor(e => e.MemberId, invitationSent.Data.MemberId);
        }
    }
}
