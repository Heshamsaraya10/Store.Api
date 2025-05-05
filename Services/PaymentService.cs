using AutoMapper;
using Domain.Contracts;
using Domain.Entities.OrderEntities;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Abstractions;
using Shared.BasketDtos;
using Shared.OrderDtos;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PaymentService (IUnitOfWork unitOfWork,
        IBasketRepository basketRepository,
        IMapper mapper , IConfiguration configuration) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = configuration.GetRequiredSection("Stripe")["SecretKey"];

               var basket = await basketRepository.GetBasketAsync(basketId);

            if (basket is null)
                throw new BasketNotFoundException(basketId);

            var productRepo = unitOfWork.GetRepository<Domain.Entities.Product, int>();

            foreach (var item in basket.Items)
            {
                var product = await productRepo.GetAsync(item.Id);

                if (product is null)
                    throw new ProductNotFoundException(item.Id);

                item.Price = product.Price;
            }
            //DeliveryMethod
            if (!basket.DeliveryMethodId.HasValue)
                throw new ArgumentNullException();
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId.Value);

            if (deliveryMethod is null)
                throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);

            basket.ShippingPrice = deliveryMethod.Price;

            var amount =(long)(basket.Items.Sum(item => item.Price * item.Quantity) + basket.ShippingPrice) * 100;

            var service = new PaymentIntentService();

            if (string.IsNullOrWhiteSpace(basket.PaymentIntentId)) //Create
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    PaymentMethodTypes = ["card"],
                    Currency = "USD",
                };

                var PaymentIntent = await service.CreateAsync(options);

                basket.PaymentIntentId = PaymentIntent.Id;
                basket.ClientSecret = PaymentIntent.ClientSecret;
            }
            else //Update
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount,
                };

                await service.UpdateAsync(basket.PaymentIntentId, options);
            }

            await basketRepository.UpdateBasketAsync(basket);
            return mapper.Map<BasketDto>(basket);
        }

        public Task UpdateOrderPaymentStatusAsync(string request, string stripeHeader)
        {
            throw new NotImplementedException();
        }
    }
}
