﻿using DemoUserManagementQuerySide.EventsHandler.Rejected;
using DemoUserManagementQuerySide.EventsHandler.Sent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagmentQuery.Test.Fakers.Rejected
{
    public class InvitationRejectedFaker : EventFaker<InvitationRejected, InvitationRejectedData>
    {
        public InvitationRejectedFaker(InvitationSent invitationSent)
        {
            RuleFor(e => e.AggregateId, invitationSent.AggregateId);
            RuleFor(e => e.Sequence, invitationSent.Sequence + 1);
            RuleFor(e => e.Data, new InvitationRejectedDataFaker(invitationSent));
        }
    }
}
