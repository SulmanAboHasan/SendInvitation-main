﻿using DemoUserManagementQuerySide.EventsHandler.Joined;
using DemoUserManagmentQuery.Test.Fakers.EntitiesFakers;

namespace DemoUserManagmentQuery.Test.Fakers.Joined
{
    public class MemberJoinedDataFaker : RecordFaker<MemberJoinedData>
    {
        private readonly string SubscriptionId;
        private readonly string MemberId;

        public MemberJoinedDataFaker()
        {
            SubscriptionId = Guid.NewGuid().ToString();
            MemberId = Guid.NewGuid().ToString();

            RuleFor(e => e.AccountId, faker => faker.Random.Guid().ToString());
            RuleFor(e => e.SubscriptionId, SubscriptionId);
            RuleFor(e => e.MemberId, MemberId);
            RuleFor(e => e.Permissions, () => new PermissionFaker()
            .WithKnownAggregate($"{SubscriptionId}_{MemberId}"));
        }
    }
}