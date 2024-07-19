using ChecklistVeiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace ChecklistVeiculos.Persistence.Repositories
{
    public class Repository<T,TContext> : IGenericRepository<T> where T : class, IModelBase  where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(TContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>?> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {

            var result = await _dbSet.FirstOrDefaultAsync<T>( p=>p.Id == id );
            if (result == null || result.Id == 0)
            {
                return null;
            }
            return result;
        }

        public async Task<T> Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool?> Update(T entity, T newValuesEntity)
        {
            _dbSet.Update(entity);
            
            _dbSet.Entry(entity).CurrentValues.SetValues(newValuesEntity);
            
            var updatedCount = await _context.SaveChangesAsync();
            if (updatedCount == 0)
            {
                return null;
            }
            return true;
        }

        public async Task<bool?> Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return null;
            }
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}