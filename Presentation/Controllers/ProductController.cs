using Microsoft.AspNetCore.Mvc;
using NHibernate.Engine;
using NHibernate.Mapping.ByCode.Impl;
using Services.Abstractions;
using Shared.ProductDto;
using System.Reflection;


namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController(IServiceManager serviceManager) : ApiController
    {
        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProductResultDto>>> GetAllProducts()
        {
            var products = await serviceManager.ProductService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet]
        public async Task<ActionResult<ProductResultDto>> GetProducts(int id)
        {
            var product = await serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands()
        {
            var brands = await serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypes()
        {
            var Types = await serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }

    }
}
