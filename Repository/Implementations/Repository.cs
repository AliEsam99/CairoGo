using CairoGo.Models.DbContextApp;
using CairoGo.Repository.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CairoGo.Repository.Implementations
{
    public class Repository<T> : IBaseRepo<T> where T : class
    {
        protected readonly CairoGoDbContext _db;
        protected readonly DbSet<T>  _DbSet;

        public Repository(CairoGoDbContext Db)
        {
            _db = Db;
            _DbSet = _db.Set<T>();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _DbSet.ToListAsync();
        }
        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _DbSet.FindAsync(id);
        }

        public async Task AddAsync(T item)
        {
             await _DbSet.AddAsync(item);
            await _db.SaveChangesAsync();

        }
        public async Task UpdateAsync(T item)
        {
          _DbSet.Update(item);
            await _db.SaveChangesAsync();
        }


        public async Task DeleteAsync(Guid id)
        {
            var object1=await _DbSet.FindAsync(id);

            if(object1 == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found");
            }
            _DbSet.Remove(object1);
            await _db.SaveChangesAsync();

        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _DbSet.Where(predicate).ToListAsync();
        }


    }
}
