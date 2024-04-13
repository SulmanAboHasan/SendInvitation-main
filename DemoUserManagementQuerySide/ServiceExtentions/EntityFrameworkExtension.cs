using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagementQuerySide.Infrastructure;
using DemoUserManagementQuerySide.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;


namespace DemoUserManagementQuerySide.ServiceExtentions
{
    public static class EntityFrameworkExtension
    {
        public static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<InvitationDbContext>(
                options => options.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
