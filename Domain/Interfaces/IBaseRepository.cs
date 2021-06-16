using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task<int> GetAllCount();
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
