using DemoUsersManagementCommandSide.Infrastructuer.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoUsersManagementCommand.Test.Helper
{
    public static class ServiceCollectionExtensions
    {
        public static void ReplaceWithInMemoryDatabase(this IServiceCollection services)
        {
            var ef = services.Single(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            services.Remove(ef);
            var dbName = Guid.NewGuid().ToString();
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(dbName));
        }
    }
}
