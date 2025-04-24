using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.BasketDtos
{
    public record BasketItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }

        [Range(1,double.MaxValue, ErrorMessage = "Price must be greater than 0" )]
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        [Range(1 , 10)]
        public int Quantity { get; set; }
    }
}
