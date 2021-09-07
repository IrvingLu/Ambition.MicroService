using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NMS.User.Domain.Tenant;

namespace NMS.User.Infrastructure.EntityTypeConfiguration
{
    public class TenantEntityTypeConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}
