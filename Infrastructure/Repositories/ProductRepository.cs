using Application.Filters;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public  class ProductRepository:BaseRepository<Product>,IProductRepository
    {
        public ProductRepository(ApplicationDbContext db):base(db)
        {

        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int pageNumber, int pageSize,decimal minPrice,decimal MaxPrice,string name,string sortBy,string sortDirection)
        {
                var products = FindByCondition(c => c.Price >= minPrice && c.Price <= MaxPrice);

            if (!string.IsNullOrEmpty(sortBy))
            {
                var colSelector = new Dictionary<string, Expression<Func<Product, object>>>
                {
                    {nameof(Product.Name),c=>c.Name},
                    {nameof(Product.Price),c=>c.Price},
                };
                var selectedColumn = colSelector[sortBy];
                products = sortDirection == SortDirection.ASC.ToString() ?
                    products.OrderBy(selectedColumn) : products.OrderByDescending(selectedColumn);
            }

            if (!products.Any() || string.IsNullOrWhiteSpace(name)) return await products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return await products.Where(c => c.Name.ToLower().Contains(name.Trim().ToLower())).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
      
        
        }
    
        public async Task CreateProductAsync(Product product)
        {
            await Create(product);
        }

        public async Task DeleteProductAsync(Product product)
        {
            await Delete(product);
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await FindByCondition(c => c.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            await Update(product);
        }

        public async Task<int> GetAllCountAsync()
        {
            return await GetAllCount();
        }

        public async Task<decimal> GetMaxPrice()
        {
            return await FindAll().MaxAsync(c => c.Price);
        }
    }
}
