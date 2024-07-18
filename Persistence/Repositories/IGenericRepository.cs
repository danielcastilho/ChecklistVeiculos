using ChecklistVeiculos.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChecklistVeiculos.Persistence.Repositories
{
    public interface IGenericRepository<T> where T: IModelBase
    {
        Task<IEnumerable<T>?> GetAll();
        Task<T?> GetById(int id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
    }
}