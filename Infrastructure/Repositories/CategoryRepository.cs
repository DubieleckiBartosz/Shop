using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CategoryRepository: BaseRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext db):base(db)
        {

        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await FindAll().ToListAsync(); 
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            var createdCategory = await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return createdCategory.Entity;
        }

        public async Task<Category> GetCategoryWithProductsAsync(int id)
        {
            return await FindByCondition(c => c.Id.Equals(id)).Include(c => c.Products).FirstOrDefaultAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await FindByCondition(c => c.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await Update(category);
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            await Delete(category);
        }
    }
}
