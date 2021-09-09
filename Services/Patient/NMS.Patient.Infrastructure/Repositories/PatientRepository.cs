using Shared.Domain.Abstractions;
using Shared.Infrastructure.Core.Repositories;

namespace NMS.Patient.Infrastructure.Repositories
{
    public class PatientRepository<TEntity> : Repository<TEntity, ApplicationDbContext>, IPatientRepository<TEntity> where TEntity : Entity
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
