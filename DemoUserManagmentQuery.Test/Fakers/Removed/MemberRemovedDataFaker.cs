using DemoUserManagementQuerySide.EventsHandler.Joined;
using DemoUserManagementQuerySide.EventsHandler.Removed;

namespace DemoUserManagmentQuery.Test.Fakers.Removed
{
    public class MemberRemovedDataFaker : RecordFaker<MemberRemovedData>
    {
        public MemberRemovedDataFaker(MemberJoined memberJoined)
        {
            RuleFor(e => e.Id, memberJoined.AggregateId);
            RuleFor(e => e.AccountId, faker => faker.Random.Guid().ToString());
            RuleFor(e => e.SubscriptionId, memberJoined.Data.SubscriptionId);
            RuleFor(e => e.MemberId, memberJoined.Data.MemberId);
        }
    }
}