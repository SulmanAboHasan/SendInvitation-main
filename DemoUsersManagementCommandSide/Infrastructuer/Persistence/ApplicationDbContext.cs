using DemoUsersManagementCommandSide.Events;
using DemoUsersManagementCommandSide.Events.DataType;
using DemoUsersManagementCommandSide.Infrastructuer.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DemoUsersManagementCommandSide.Infrastructuer.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): DbContext(options)
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OutboxMessageConfigurations());
            modelBuilder.ApplyConfiguration(new BaseEventConfiguration());
            modelBuilder.ApplyConfiguration(new GenericEventConfiguration<InvitationSent, InvitationSentData>());
            modelBuilder.ApplyConfiguration(new GenericEventConfiguration<InvitationAccepted, InvitationAcceptedtData>());
            modelBuilder.ApplyConfiguration(new GenericEventConfiguration<InvitationCanceled, InvitationCanceledData>());
            modelBuilder.ApplyConfiguration(new GenericEventConfiguration<InvitationRejected, InvitationRejectedtData>());
            modelBuilder.ApplyConfiguration(new GenericEventConfiguration<MemberJoined, MemberJoinedData>());
            modelBuilder.ApplyConfiguration(new GenericEventConfiguration<MemberRemoved, MemberRemovedData>());
            modelBuilder.ApplyConfiguration(new GenericEventConfiguration<MemberLeft, MemberLeftData>());
            modelBuilder.ApplyConfiguration(new GenericEventConfiguration<PermissionChanged, PermissionChangedData>());
        }
    }
}
