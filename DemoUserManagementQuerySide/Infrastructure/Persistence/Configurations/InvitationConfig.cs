using DemoUserManagementQuerySide.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoUserManagementQuerySide.Infrastructure.Persistence.Configurations
{
    public class InvitationConfig : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.Property(i => i.Sequence).IsConcurrencyToken();
        }
    }
}
