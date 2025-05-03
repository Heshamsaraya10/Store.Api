using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderDtos
{
    public record OrderRequest
    {
        public string BasketId { get; set; }

        public AddressDto ShipingAddress { get; set; }

        public int DeliveryMethodId { get; set; }
    }
}
