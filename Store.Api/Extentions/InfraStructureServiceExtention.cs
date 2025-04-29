using Domain.Contracts;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presistence.Data;
using Presistence.Identity;
using Presistence.Repositories;
using StackExchange.Redis;
using System.Text.Json.Serialization;

namespace Store.Api.Extentions
{
    public static class InfraStructureServiceExtention
    {
        public static IServiceCollection AddInfraStructureService(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();

            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultSQlConnection"));
            });

            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentitySQlConnection"));
            });

            services.AddSingleton<IConnectionMultiplexer>
                (_ => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"))
            );
            services.ConfigureIdentity();

            return services;

        }

        private  static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<StoreIdentityDbContext>();

            return services;
        }
    }
}
