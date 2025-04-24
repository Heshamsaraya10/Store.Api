using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using Shared.ProductDto;


namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork , IMapper mapper) : IProductService
    {
      
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();

            var MappBrands = mapper.Map<IEnumerable<BrandResultDto>>(brands);

            return MappBrands; 
        }

        public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParams SpecificationParams)
        {
            var specs = new ProductWithFilterSpecification(SpecificationParams); 

            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(specs);

            var countSpecs = new ProductCountSpecification(SpecificationParams);

            var totalCount = await unitOfWork.GetRepository<Product, int>().CountAsync(countSpecs);

            var MappProducts = mapper.Map<IEnumerable<ProductResultDto>>(products);

            return new PaginatedResult<ProductResultDto>
                (SpecificationParams.PageIndex , SpecificationParams.PageSize , totalCount, MappProducts);
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();

            var MappTypes = mapper.Map<IEnumerable<TypeResultDto>>(types);

            return MappTypes;
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            try
            {
                var specs = new ProductWithFilterSpecification(id);
                var product = await unitOfWork.GetRepository<Product, int>().GetAsync(specs);

                return product is null ? throw new ProductNotFoundException(id) : mapper.Map<ProductResultDto>(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
