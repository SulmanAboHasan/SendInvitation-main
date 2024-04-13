using DemoUserManagementQuerySide.EventsHandler.Joined;
using DemoUserManagementQuerySide.EventsHandler.Removed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagmentQuery.Test.Fakers.Removed
{
    public class MemberRemovedFaker : EventFaker<MemberRemoved, MemberRemovedData>
    {
        public MemberRemovedFaker(MemberJoined memberJoined)
        {
            RuleFor(e => e.AggregateId, memberJoined.AggregateId);
            RuleFor(e => e.Sequence, memberJoined.Sequence + 1);
            RuleFor(e => e.Data, new MemberRemovedDataFaker(memberJoined));
        }
    }
}
