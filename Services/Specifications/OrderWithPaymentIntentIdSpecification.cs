using Domain.Contracts;
using Domain.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderWithPaymentIntentIdSpecification(string PaymentIntentId) :
        Specification<Order>(x => x.PaymentIntentId == PaymentIntentId)
    {

    }
}
