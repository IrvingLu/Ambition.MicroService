using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Abstractions.Identity;

namespace Pet.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){
        
        }
        #region 数据库

        public DbSet<Domain.PetBase.Pet> Pet { get; set; }
        public DbSet<Domain.PetBase.Pet_MedicalHistory> Pet_MedicalHistory { get; set; }
        public DbSet<Domain.PetBase.Pet_VaccineRecord> Pet_VaccineRecord { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 注册领域模型与数据库的映射关系
            //modelBuilder.ApplyConfiguration(new PetEntityTypeConfiguration());
            #endregion
            base.OnModelCreating(modelBuilder);
        }

   
    }
}
