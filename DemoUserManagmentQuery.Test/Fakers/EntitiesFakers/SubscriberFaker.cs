﻿using DemoUserManagementQuerySide.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagmentQuery.Test.Fakers.EntitiesFakers
{
    public class SubscriberFaker : RecordFaker<Subscriper>
    {
        public SubscriberFaker()
        {
            RuleFor(i => i.Id, faker => faker.Random.Guid().ToString());
            RuleFor(i => i.Sequence, faker => faker.Random.Int(1, 9));
            RuleFor(i => i.Subscription, new SubscriptionFaker());
            RuleFor(i => i.SubscriptionId, faker => faker.Random.Guid().ToString());
            RuleFor(i => i.User, new UserFaker());
            RuleFor(i => i.UserId, faker => faker.Random.Guid().ToString());
            RuleFor(i => i.Status, "Joined");
            RuleFor(i => i.JoinedAt, DateTime.UtcNow);
        }

        public List<Subscriper> SameUserDiffrentSubscription(string userId, int count)
        {
            RuleFor(i => i.UserId, userId);

            return RuleFor(i => i.User, new UserFaker()
                .RuleFor(u => u.Id, userId))
            .Generate(count);
        }

        public List<Subscriper> SameSupscriptionDiffrentUser(string subscriptionId, int count)
        {
            RuleFor(i => i.SubscriptionId, subscriptionId);

            return RuleFor(i => i.Subscription, new SubscriptionFaker()
                .RuleFor(u => u.Id, subscriptionId))
                .Generate(count);
        }
    }
}
