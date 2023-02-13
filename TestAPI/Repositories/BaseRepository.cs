using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Models;

namespace TestAPI.Repositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        Task<List<T>> ListAsync();
        ValueTask<T> DetailAsync(int id);
        Task<bool> SaveAsync(T data);
        Task<bool> UpdateAsync(int id, T data);
        Task<bool> DeleteAsync(int id);
    }

    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        private readonly TestAPIContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(IDbContextFactory<TestAPIContext> context)
        {
            _context = context.CreateDbContext();
            _dbSet = _context.Set<T>();
        }

        public Task<List<T>> ListAsync()
        {
            return _dbSet.ToListAsync();
        }

        public ValueTask<T> DetailAsync(int id)
        {
            return _dbSet.FindAsync(id);
        }

        public async Task<bool> SaveAsync(T data)
        {
            _dbSet.Add(data);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, T data)
        {
            _dbSet.Update(data);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            T item = await _dbSet.FindAsync(id);
            if (item == null)
            {
                return false;
            }

            _context.Remove<T>(item);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
