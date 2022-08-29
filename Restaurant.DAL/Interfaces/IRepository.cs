using System.Linq.Expressions;

namespace Restaurant.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        //methods
        // GetAll, GetById, Add, Remove , AddRange, RemoveRange
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties=null);
        Task<T> GetById(int? id);  
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);  
    }
}
