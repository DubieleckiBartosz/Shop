using Application.DtoModels.ProductModels;
using Application.Exceptions;
using Application.Filters;
using Application.Service.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Service
{
    class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IProductRepository productRepository,ICategoryRepository categoryRepository,IMapper mapper,ILogger<ProductService> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task AddProductAsync(int categoryId, CreateProductDto productDto)
        {
            if (productDto is null) throw new BadRequestException("Product sent from client is null");
            var category=await _categoryRepository.GetCategory(categoryId);
            
            if (category is null) throw new NotFoundException("Not found category");
            
            var productDb = _mapper.Map<Product>(productDto);           
            productDb.CategoryId = categoryId;           
            await _productRepository.CreateProductAsync(productDb);
            _logger.LogInformation($"Create new product with name: {productDto.Name}");
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetProductAsync(id);
            if (product is null) throw new NotFoundException("Not found product to delete");
            
                await _productRepository.DeleteProductAsync(product);
            _logger.LogInformation($"Deleted product with id: {id}");
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts(ProductParameters productParameters)
        {           
            if(productParameters.MaxPrice is 0)
            {
                productParameters.MaxPrice =await _productRepository.GetMaxPrice();
            }
            if (!productParameters.ValidPriceParameters) throw new BadRequestException("Max price cannot be less than min price");
            var products =await _productRepository.GetProductsAsync(productParameters.PageNumber, productParameters.PageSize,productParameters.MinPrice,
                productParameters.MaxPrice,productParameters.Name,productParameters.SortBy,productParameters.SortDirection.ToString());
      
            if (products is null) throw new NotFoundException("Not found products");
            _logger.LogInformation($"Returned all products");
            
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<int> GetAllProductsCountAsync()
        {
           return await _productRepository.GetAllCountAsync();
        }

        public async Task UpdateProductAsync(int id,UpdateProductDto productDto)
        {
            if (productDto is null) throw new BadRequestException("Product sent from client is null");
            var product = await _productRepository.GetProductAsync(id);
            if (product is null) throw new NotFoundException("Not found product");
            _mapper.Map(productDto, product);
            await _productRepository.UpdateProductAsync(product);
            _logger.LogInformation($"Updated product with id: {id}");
        }
    }
}
