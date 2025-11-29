using System.Linq.Expressions;

namespace CairoGo.Repository.Interfaces
{
    public interface IBaseRepo<T> where T : class 
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(Guid id);

        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
