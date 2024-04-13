using DemoUserManagementQuerySide.EventsHandler.Changed;
using DemoUserManagementQuerySide.EventsHandler.Joined;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagmentQuery.Test.Fakers.Changed
{
    public class PermissionChangedFaker : EventFaker<PermissionChanged, PermissionChangedData>
    {
        public PermissionChangedFaker(MemberJoined memberJoined)
        {
            RuleFor(e => e.AggregateId, memberJoined.AggregateId);
            RuleFor(e => e.Sequence, memberJoined.Sequence + 1);
            RuleFor(e => e.Data, new PermissionChangedDataFaker(memberJoined));
        }
    }
}
