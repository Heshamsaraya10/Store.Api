using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Presistence.Data;
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

        public DbInitializer(StoreDbContext context)
        {
            _context = context;
        }
        public async Task  InitializeAsync()
        {
            try
            {
                //Check if the database is empty
                //if (_context.Database.GetPendingMigrations().Any())
                //{
                //    _context.Database.Migrate();
                //}


                if (!_context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText(@"..\Presistence\Data\Seeding\types.json");

                    //Convert string to list of ProductType
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    if (types is not null && types.Any())
                    {
                      await  _context.ProductTypes.AddRangeAsync(types);
                      await  _context.SaveChangesAsync();
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
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error initializing the database", ex);
            }
        }
    }
}
