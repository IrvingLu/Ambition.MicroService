using Shared.Domain.Abstractions;
using Shared.Infrastructure.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMS.Patient.Infrastructure.Repositories
{
    public class PatientRepository<TEntity> : Repository<TEntity, ApplicationDbContext>, IPatientRepository<TEntity> where TEntity : Entity
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
