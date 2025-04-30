using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Entities.OrderEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Presistence.Data;
using Presistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;
        private readonly StoreIdentityDbContext _identityDbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public DbInitializer(StoreDbContext context ,
            StoreIdentityDbContext identityDbContext ,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            _context = context;
            _identityDbContext = identityDbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task  InitializeAsync()
        {
            try
            {
                //Check if the database is empty
                if (_context.Database.GetPendingMigrations().Any())
                {
                    _context.Database.Migrate();
                }


                if (!_context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText(@"..\Presistence\Data\Seeding\types.json");

                    //Convert string to list of ProductType
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    if (types is not null && types.Any())
                    {
                        await _context.ProductTypes.AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }
                }

                if (!_context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText(@"..\Presistence\Data\Seeding\brands.json");

                    //Convert string to list of ProductBrands
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    if (brands is not null && brands.Any())
                    {
                        await _context.ProductBrands.AddRangeAsync(brands);
                        await _context.SaveChangesAsync();
                    }
                }

                if (!_context.Products.Any())
                {
                    var productsData = File.ReadAllText(@"..\Presistence\Data\Seeding\products.json");

                    //Convert string to list of Products
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    if (products is not null && products.Any())
                    {
                        await _context.Products.AddRangeAsync(products);
                        await _context.SaveChangesAsync();
                    }

                    if (!_context.DeliveryMethods.Any())
                    {
                        var deliveryData = File.ReadAllText(@"..\Presistence\Data\Seeding\delivery.json");

                        //Convert string to list of Products
                        var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);

                        if (deliveryMethods is not null && deliveryMethods.Any())
                        {
                            await _context.DeliveryMethods.AddRangeAsync(deliveryMethods);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error initializing the database", ex);
            }
        }

        public async Task InitializeIdentityAsync()
        {
            if (_identityDbContext.Database.GetPendingMigrations().Any())
               await _identityDbContext.Database.MigrateAsync();
            
            if(!_roleManager.Roles.Any())
            {
               await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
               await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
            }

            if (!_userManager.Users.Any())
            {
                var superAdminUser = new User
                {
                    DisplayName = "Super Admin",
                    Email = "SuberAdmin@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "123456789",
                };

                var adminUser = new User
                {
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "987654321",
                };


                await _userManager.CreateAsync(superAdminUser, "Passw0rd");
                await _userManager.CreateAsync(adminUser, "Passw0rd");

                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(adminUser, "Admin");

            }
        }
    }
}
 