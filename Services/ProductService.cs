using AutoMapper;
using Domain;
using Domain.Entities;
using Services.Abstractions;
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

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {
           var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync();

            var MappProducts = mapper.Map<IEnumerable<ProductResultDto>>(products);

            return MappProducts;
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
                var product = await unitOfWork.GetRepository<Product, int>().GetAsync(id);

                var MappProduct = mapper.Map<ProductResultDto>(product);

                return MappProduct;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
