using DemoUserManagementQuerySide.Entities;
using DemoUserManagementQuerySide.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DemoUserManagementQuerySide.Infrastructure
{
    public class InvitationDbContext(DbContextOptions<InvitationDbContext> options): DbContext(options)
    {
        public DbSet<Invitation> invitations { get; set; }
        public DbSet<Permission> permissions { get; set; }
        public DbSet<Subscriper> subscripers { get; set; }
        public DbSet<Subscription> subscriptions { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InvitationConfig());
            modelBuilder.ApplyConfiguration(new PermissionConfig());
            modelBuilder.ApplyConfiguration(new SubscriperConfig());
        }
    }
}
