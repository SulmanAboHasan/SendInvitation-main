using DemoUserManagementQuerySide.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUserManagmentQuery.Test.Helper
{
    public static class ServiceCollectionExtensions
    {
        public static void ReplaceWithInMemoryDatabase(this IServiceCollection services)
        {
            var ef = services.Single(d => d.ServiceType == typeof(DbContextOptions<InvitationDbContext>));
            services.Remove(ef);
            var dbName = Guid.NewGuid().ToString();
            services.AddDbContext<InvitationDbContext>(options => options.UseInMemoryDatabase(dbName));
        }
    }
}
