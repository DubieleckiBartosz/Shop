using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Auth;
using WebAPI.Auth.Interfaces;

namespace WebAPI.Extensions
{
    public static class ExtensionDependency
    {
        public static void ConfigureDependency(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IUserOperation, UserOperation>();
        }
    }
}
