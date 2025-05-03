using Domain.Contracts;
using Domain.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderWithIncludeSpecification : Specification<Order>
    {

        public OrderWithIncludeSpecification(Guid id)
            : base(order => order.Id == id)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
        }


        public OrderWithIncludeSpecification(string email)
            : base(order => order.BuyerEmail == email)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);

            SetOrderByDescending(order => order.OrderDate);
        }
    }
}
