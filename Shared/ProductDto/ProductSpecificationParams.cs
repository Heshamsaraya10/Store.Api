using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ProductDto
{
    public class ProductSpecificationParams
    {
        private const int MAX_PAGE_SIZE = 50;

        private const int DEFULT_PAGE_SIZE = 5;

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        public string? Search { get; set; }

        public string?  Sort { get; set; }
        
        public int PageIndex { get; set; } = 1;


        
        private int _PageSize = DEFULT_PAGE_SIZE;

        public int PageSize
        {
            get => _PageSize;
            set => _PageSize = value > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : value;
        }

    }
}
