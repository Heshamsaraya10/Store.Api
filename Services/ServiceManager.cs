using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstractions;
using Shared.IdentityDtos;
using Stripe.Climate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IAuthenticationServices> _authenticationServices;
        private readonly Lazy<IOrderService> _orderService;
        private readonly Lazy<IPaymentService> _paymentService;
        private readonly Lazy<ICachService> _cachService;



        public ServiceManager(
            IUnitOfWork unitOfWork,
            IMapper mapper ,
            IBasketRepository basketRepository , UserManager<User> userManager,
            IConfiguration configuration,
            ICachRepository cachRepository,
            IOptions<JwtOptions> options
            )
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork , mapper));
            _basketService = new Lazy<IBasketService>(() => new BasketService(basketRepository,mapper));
            _authenticationServices = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(userManager, mapper , options));
            _orderService = new Lazy<IOrderService>(() => new OrderService(unitOfWork , mapper , basketRepository));
            _paymentService = new Lazy<IPaymentService>(() => new PaymentService(unitOfWork, basketRepository, mapper, configuration));
            _cachService = new Lazy<ICachService>(() => new CachService(cachRepository));



        }
        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthenticationServices AuthenticationService => _authenticationServices.Value;

        public IOrderService OrderService => _orderService.Value;

        public IPaymentService PaymentService => _paymentService.Value;

        public ICachService CachService => _cachService.Value;
    }
}
