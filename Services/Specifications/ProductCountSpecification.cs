using Domain.Contracts;
using Domain.Entities;
using Shared.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductCountSpecification : Specification<Product>
    {
    
        public ProductCountSpecification(ProductSpecificationParams specs)
            : base(Product => (!specs.BrandId.HasValue || Product.BrandId == specs.BrandId) &&
                            (!specs.TypeId.HasValue || Product.TypeId == specs.TypeId) &&
                            (string.IsNullOrWhiteSpace(specs.Search) ||
                            Product.Name.ToLower().Contains(specs.Search.ToLower().Trim()))
            )
        {

        }

    }
}
