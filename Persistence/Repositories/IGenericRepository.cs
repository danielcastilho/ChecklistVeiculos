using ChecklistVeiculos.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChecklistVeiculos.Persistence.Repositories
{
    public interface IGenericRepository<T> where T: IModelBase
    {
        Task<IEnumerable<T>?> GetAll();
        Task<T?> GetById(int id);
        Task<T> Create(T entity);
        Task<bool?> Update(T entity, T newValuesEntity);
        Task<bool?> Delete(int id);
    }
}