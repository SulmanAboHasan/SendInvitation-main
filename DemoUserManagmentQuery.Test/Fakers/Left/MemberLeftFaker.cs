using DemoUserManagementQuerySide.EventsHandler.Joined;
using DemoUserManagementQuerySide.EventsHandler.Left;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagmentQuery.Test.Fakers.Left
{
    public class MemberLeftFaker : EventFaker<MemberLeft, MemberLeftData>
    {
        public MemberLeftFaker(MemberJoined memberJoined)
        {
            RuleFor(e => e.AggregateId, memberJoined.AggregateId);
            RuleFor(e => e.Sequence, memberJoined.Sequence + 1);
            RuleFor(e => e.Data, new MemberLeftDataFaker(memberJoined));
        }
    }
}
