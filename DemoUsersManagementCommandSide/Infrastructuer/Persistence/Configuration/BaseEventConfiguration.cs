using DemoUsersManagementCommandSide.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoUsersManagementCommandSide.Infrastructuer.Persistence.Configuration
{
    public class BaseEventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasIndex(e => new { e.aggregateId, e.sequence }).IsUnique();
            builder.Property<string>("EventType").HasMaxLength(128);
            builder.HasDiscriminator<string>("EventType");
        }
    }
}
