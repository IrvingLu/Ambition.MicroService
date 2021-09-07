using Shared.Domain.Abstractions;
using Shared.Infrastructure.Core.Repositories;

namespace NMS.Patient.Infrastructure.Repositories
{
    public interface IPatientRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {

    }
}
