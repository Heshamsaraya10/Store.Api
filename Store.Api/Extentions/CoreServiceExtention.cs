using Services.Abstractions;
using Services.MappingProfiles;
using Services;
using Shared.IdentityDtos;

namespace Store.Api.Extentions
{
    public static class CoreServiceExtention
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(typeof(ProductProfile).Assembly);
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            return services;
        }
    }
}
