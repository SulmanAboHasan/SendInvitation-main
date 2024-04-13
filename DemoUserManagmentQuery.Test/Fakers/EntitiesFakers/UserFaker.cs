using DemoUserManagementQuerySide.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagmentQuery.Test.Fakers.EntitiesFakers
{
    public class UserFaker : RecordFaker<User>
    {
        public UserFaker()
        {
            RuleFor(i => i.Id, faker => faker.Random.Guid().ToString());
            RuleFor(i => i.Name, faker => $"User{faker.Random.Int()}");
        }
    }
}
