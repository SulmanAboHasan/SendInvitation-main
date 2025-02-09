﻿using DemoUserManagementQuerySide.EventsHandler.Accepted;
using DemoUserManagementQuerySide.EventsHandler.Sent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagmentQuery.Test.Fakers.Accepted
{
    public class InvitationAcceptedFaker : EventFaker<InvitationAccepted, InvitationAcceptedData>
    {
        public InvitationAcceptedFaker(InvitationSent invitationSent)
        {
            RuleFor(e => e.AggregateId, invitationSent.AggregateId);
            RuleFor(e => e.Sequence, invitationSent.Sequence + 1);
            RuleFor(e => e.Data, new InvitationAcceptedDataFaker(invitationSent));
        }
    }
}
