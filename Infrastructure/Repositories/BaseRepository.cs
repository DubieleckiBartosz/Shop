using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _db;
        public BaseRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(T entity)
        {
             await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<T> FindAll()
        {
            return _db.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _db.Set<T>().Where(expression);
        }

        public async Task<int> GetAllCount()
        {
            return await _db.Set<T>().CountAsync();
        }

        public async Task Update(T entity)
        {
           _db.Set<T>().Update(entity);
            await _db.SaveChangesAsync();
            await Task.CompletedTask;
        }
    }
}
