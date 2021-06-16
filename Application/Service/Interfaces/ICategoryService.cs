using Application.DtoModels.CategoryModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryAsync(int categoryId);
        Task<CategoryDto> AddCategoryAsync(CreateCategoryDto categoryDto);
        Task DeleteCategoryAsync(int id);
        Task UpdateCategoryAsync(int id,UpdateCategoryDto categoryDto);
    }
}
