using Application.DtoModels.CategoryModels;
using Application.DtoModels.OrderLineModels;
using Application.DtoModels.OrderModels;
using Application.DtoModels.PictureModels;
using Application.DtoModels.ProductModels;
using AutoMapper;
using Domain.Entities;

namespace Application
{
    public static class MapperConfig
    {
        public static IMapper Initialize() =>
            new MapperConfiguration(config =>
            {
                config.CreateMap<Category, CategoryDto>();
                config.CreateMap<CreateCategoryDto, Category>();
                config.CreateMap<UpdateCategoryDto, Category>();

                config.CreateMap<Product, ProductDto>();
                config.CreateMap<CreateProductDto, Product>();
                config.CreateMap<UpdateProductDto, Product>();

                config.CreateMap<OrderLine, OrderLineDto>()
                .ForMember(c => c.Name, s => s.MapFrom(x => x.Product.Name))
                .ForMember(c => c.Price, s => s.MapFrom(x => x.Product.Price));

                config.CreateMap<CreateOrderLineDto, OrderLine>();

                config.CreateMap<CreateOrderDto, Order>()
                .ForMember(c => c.Address, m => m.MapFrom(d => new Address() { City = d.City, PostalCode = d.PostalCode, Street = d.Street }));
                config.CreateMap<Order, OrderDto>()
                .ForMember(c => c.DateCreated, s => s.MapFrom(p => p.CreatedDate))
                .ForMember(c => c.City, s => s.MapFrom(p => p.Address.City))
                .ForMember(c => c.Street, s => s.MapFrom(p => p.Address.Street))
                .ForMember(c => c.PostalCode, s => s.MapFrom(p => p.Address.PostalCode));

                config.CreateMap<UpdateOrderLineDto, OrderLine>();
             
                
                config.CreateMap<Picture, PictureDto>();
            }).CreateMapper();
    }
}
