using DemoUserManagementQuerySide.EventsHandler.IncrementSequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagmentQuery.Test.Fakers.IncrementSequence
{
    public class UnknownEventFaker : EventFaker<UnknownEvent, object>
    {
        public UnknownEventFaker(int sequence)
        {
            RuleFor(e => e.AggregateId, Guid.NewGuid().ToString());
            RuleFor(e => e.Sequence, sequence);
            RuleFor(e => e.DateTime, DateTime.UtcNow);
            RuleFor(e => e.UserId, Guid.NewGuid().ToString());
            RuleFor(e => e.Version, 1);
            RuleFor(e => e.Data, new object());
        }
    }
}
