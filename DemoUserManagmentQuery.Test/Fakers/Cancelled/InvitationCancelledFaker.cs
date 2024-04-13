using DemoUserManagementQuerySide.EventsHandler.Cancelled;
using DemoUserManagementQuerySide.EventsHandler.Sent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagmentQuery.Test.Fakers.Cancelled
{
    public class InvitationCancelledFaker : EventFaker<InvitationCancelled, InvitationCancelledData>
    {
        public InvitationCancelledFaker(InvitationSent invitationSent)
        {
            RuleFor(e => e.AggregateId, invitationSent.AggregateId);
            RuleFor(e => e.Sequence, invitationSent.Sequence + 1);
            RuleFor(e => e.Data, new InvitationCancelledDataFaker(invitationSent));
        }
    }
}
