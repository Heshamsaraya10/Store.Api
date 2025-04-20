using Shared;
using Shared.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParams SpecificationParams);
        Task<ProductResultDto> GetProductByIdAsync(int id);

        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();

        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
    }
}
