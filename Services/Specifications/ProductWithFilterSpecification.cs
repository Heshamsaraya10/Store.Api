using Domain.Contracts;
using Domain.Entities;
using Shared.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithFilterSpecification : Specification<Product>
    {
        public ProductWithFilterSpecification(int id)
            : base(Product => Product.Id == id)
        {
            AddInclude(Product => Product.ProductBrand);
            AddInclude(Product => Product.ProductType);

        }

        public ProductWithFilterSpecification(ProductSpecificationParams specs) 
            :base(Product =>( !specs.BrandId.HasValue || Product.BrandId == specs.BrandId ) &&
                            (!specs.TypeId.HasValue || Product.TypeId == specs.TypeId) &&
                            (string.IsNullOrWhiteSpace(specs.Search) ||
                            Product.Name.ToLower().Contains(specs.Search.ToLower().Trim()))
            )
        {
            AddInclude(Product => Product.ProductBrand);
            AddInclude(Product => Product.ProductType);

            
            ApplyPagination(specs.PageIndex , specs.PageSize);


            if (specs.Sort is not null)
            {
                switch (specs.Sort)
                {
                    case "nameAsc":
                        SetOrderBy(product => product.Name);
                        break;

                    case "nameDesc":
                        SetOrderByDescending(product => product.Name);
                        break;

                    case "PriceAsc":
                        SetOrderBy(product => product.Price);
                        break;

                    case "PriceDesc":
                        SetOrderByDescending(product => product.Price);
                        break;

                    default:
                        SetOrderBy(product => product.Name);
                        break;
                }
            }
        }
    }
}
