using DemoUserManagementQuerySide.EventsHandler.Joined;
using DemoUserManagementQuerySide.EventsHandler.Left;

namespace DemoUserManagmentQuery.Test.Fakers.Left
{
    public class MemberLeftDataFaker : RecordFaker<MemberLeftData>
    {
        public MemberLeftDataFaker(MemberJoined memberJoined)
        {
            RuleFor(e => e.Id, memberJoined.AggregateId);
            RuleFor(e => e.AccountId, faker => faker.Random.Guid().ToString());
            RuleFor(e => e.SubscriptionId, memberJoined.Data.SubscriptionId);
            RuleFor(e => e.MemberId, memberJoined.Data.MemberId);
        }
    }
}