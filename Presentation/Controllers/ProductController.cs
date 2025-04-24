using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.ErrorModels;
using Shared.ProductDto;
using System.Net;


namespace Presentation.Controllers
{
    //[ApiController]
    //[Route("api/[controller]/[action]")]
    public class ProductController(IServiceManager serviceManager) : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProducts([FromQuery] ProductSpecificationParams specs)
        {
            var products = await serviceManager.ProductService.GetAllProductsAsync(specs);
            return Ok(products);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProductResultDto), (int)HttpStatusCode.OK)]
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
