using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICategoryRepository:IBaseRepository<Category>
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategory(int id);
        Task<Category> GetCategoryWithProductsAsync(int id);
        Task<Category> CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
    }
}
