using Shared.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasketAsync(string id);
        Task<BasketDto> UpdateBasketAsync(BasketDto basket);

        Task<bool> DeleteBasketAsync(string id);
    }
}
