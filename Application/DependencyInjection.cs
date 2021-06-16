using Application.Service;
using Application.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPictureService, PictureService>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IRaportService, RaportService>();
        }
    }
}
