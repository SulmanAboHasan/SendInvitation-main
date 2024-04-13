using DemoUserManagementQuerySide.EventsHandler.Sent;
using DemoUserManagmentQuery.Test.Fakers.EntitiesFakers;

namespace DemoUserManagmentQuery.Test.Fakers.Sent
{
    public class InvitationSentDataFaker : RecordFaker<InvitationSentData>
    {
        private readonly string SubscriptionId;
        private readonly string MemberId;

        public InvitationSentDataFaker()
        {
            SubscriptionId = Guid.NewGuid().ToString();
            MemberId = Guid.NewGuid().ToString();

            RuleFor(e => e.AccountId, faker => faker.Random.Guid().ToString());
            RuleFor(e => e.SubscriptionId, faker => faker.Random.Guid().ToString());
            RuleFor(e => e.MemberId, faker => faker.Random.Guid().ToString());
            RuleFor(e => e.Permissions, () => new PermissionFaker()
            .WithKnownAggregate($"{SubscriptionId}_{MemberId}"));
        }
    }
}
