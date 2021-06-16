using Application.DtoModels.CategoryModels;
using Application.Exceptions;
using Application.Service.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Service
{
    class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper,ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
           var categories= await _categoryRepository.GetCategoriesAsync();
            if (categories is null) throw new NotFoundException("Not found any categories");
            _logger.LogInformation("all categories have been returned");
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryAsync(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryWithProductsAsync(categoryId);
            if (category is null) throw new NotFoundException($"Not Found category with id: {categoryId}");
            _logger.LogInformation($"Returned category with id: {categoryId}");
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> AddCategoryAsync(CreateCategoryDto categoryDto)
        {
            if (categoryDto is null) throw new BadRequestException("Category sent from client is null");

            var categoryDb = _mapper.Map<Category>(categoryDto);
           var result= await _categoryRepository.CreateCategoryAsync(categoryDb);
            _logger.LogInformation($"Added new category: {result.Name}");
            return _mapper.Map<CategoryDto>(result);
        }

        public async Task DeleteCategoryAsync(int id)
        {

            var category =await _categoryRepository.GetCategory(id);
            if (category is null) throw new NotFoundException($"Not found category with id: {id}");
            
            await _categoryRepository.DeleteCategoryAsync(category);
            _logger.LogInformation($"Deleted category with id: {id}");
        }

        public async Task UpdateCategoryAsync(int id,UpdateCategoryDto categoryDto)
        {
            if (categoryDto is null) throw new BadRequestException("Category is null, try again");
            var category =await _categoryRepository.GetCategory(id);
            if (category is null) throw new NotFoundException($"Not found category with id: {id}");
            _mapper.Map(categoryDto, category);
            await _categoryRepository.UpdateCategoryAsync(category);
            _logger.LogInformation($"Updated category with id: {id}");
        }
    }
}
