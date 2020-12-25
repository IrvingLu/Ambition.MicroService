using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pet.Reservation.Domain.Reservation;
using Pet.Reservation.Infrastructure.EntityTypeConfiguration;
using Shared.Domain.Abstractions.Identity;

namespace Pet.Reservation.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){
        
        }
        #region 数据库

        public DbSet<Domain.Reservation.Reservation> Reservation { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 注册领域模型与数据库的映射关系
            modelBuilder.ApplyConfiguration(new ReservationEntityTypeConfiguration());
            #endregion
            base.OnModelCreating(modelBuilder);
        }

   
    }
}
