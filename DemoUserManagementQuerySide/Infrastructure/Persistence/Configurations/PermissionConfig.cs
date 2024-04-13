using DemoUserManagementQuerySide.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoUserManagementQuerySide.Infrastructure.Persistence.Configurations
{
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.Property(p => p.Sequence).IsConcurrencyToken();
        }
    }
}
