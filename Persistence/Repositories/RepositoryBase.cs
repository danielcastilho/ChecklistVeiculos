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

        public async Task<T> Update(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new Exception("Entity not found");
            }
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}