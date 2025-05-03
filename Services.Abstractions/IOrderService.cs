using Shared.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IOrderService
    {
        Task<OrderResult> GetOrderByIdAsync(Guid id);

        Task<IEnumerable<OrderResult>> GetOrderEmailIdAsync(string email);

        Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest , string buyerEmail);

        Task<IEnumerable<DeliveryMethodDto>> GetdeliveryMethodDtos();


    }
}
