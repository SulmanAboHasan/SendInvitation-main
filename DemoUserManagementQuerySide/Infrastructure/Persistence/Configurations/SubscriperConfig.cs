using DemoUserManagementQuerySide.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoUserManagementQuerySide.Infrastructure.Persistence.Configurations
{
    public class SubscriperConfig : IEntityTypeConfiguration<Subscriper>
    {
        public void Configure(EntityTypeBuilder<Subscriper> builder)
        {
            builder.Property(s => s.Sequence).IsConcurrencyToken();
        }
    }
}
