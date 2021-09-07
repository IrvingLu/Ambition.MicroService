using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NMS.Patient.Infrastructure.EntityTypeConfiguration
{
    public class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Patient.Patient>
    {
        public void Configure(EntityTypeBuilder<Domain.Patient.Patient> builder)
        {
            builder.HasKey(f => f.Id);
        }
    }
}
