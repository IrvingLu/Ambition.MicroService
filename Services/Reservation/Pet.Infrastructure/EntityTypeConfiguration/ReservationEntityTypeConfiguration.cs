using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pet.Reservation.Domain.Reservation;

namespace Pet.Reservation.Infrastructure.EntityTypeConfiguration
{
    public class ReservationEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Reservation.Reservation>
    {
        public void Configure(EntityTypeBuilder<Domain.Reservation.Reservation> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}
