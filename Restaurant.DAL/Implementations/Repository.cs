using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Interfaces;
using System.Linq.Expressions;

namespace Restaurant.DAL.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        public DbSet<T> _dbSet;
        public Repository(DbContext context)
        {
            _context = context; 
            _dbSet=_context.Set<T>();   
        }
        public async Task Add(T entity)
        {
           await  _dbSet.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null)
        {
           IQueryable<T> query = _dbSet;  
            if(filter != null)
            {
                query= query.Where(filter); 
            }
            return await query.ToListAsync();

        }
        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
