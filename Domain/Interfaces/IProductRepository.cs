using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductRepository:IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsAsync(int pageNumber, int pageSize, decimal minPrice, decimal MaxPrice,string nameProduct, string sortBy, string sortDirection);
        Task<decimal> GetMaxPrice();
        Task<Product> GetProductAsync(int id);
        Task<int> GetAllCountAsync();
        Task CreateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}
