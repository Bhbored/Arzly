using Arzly.Api.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Arzly.Api.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
    where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(TKey? id)
        {
            if (id == null) return null;
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual TEntity Update(TEntity entity)
        {
            _dbSet.Update(entity);
            _context.SaveChangesAsync().GetAwaiter().GetResult();
            return entity;
        }

        public virtual bool Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChangesAsync().GetAwaiter().GetResult();
            return true;
        }
    }
}
