using Application.DtoModels.ProductModels;
using Application.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Service.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProducts(ProductParameters productParameters);
        
        Task<int> GetAllProductsCountAsync();
        Task AddProductAsync(int categoryId,CreateProductDto productDto);
        Task DeleteProductAsync(int id);
        Task UpdateProductAsync(int id,UpdateProductDto productDto);
        
    }
}
