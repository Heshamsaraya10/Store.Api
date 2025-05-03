using AutoMapper;
using Domain.Entities.OrderEntities;
using Shared.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
   public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Address , Shared.OrderDtos.AddressDto>().ReverseMap();
            
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, option => option.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.ProductName, option => option.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, option => option.MapFrom(src => src.Product.PictureUrl));


            CreateMap<Order, OrderResult>()
                .ForMember(dest => dest.PaymentStatus, option => option.MapFrom(src => src.PaymentStatus.ToString()))
                .ForMember(dest => dest.DeliveryMethod, option => option.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.Total, option => option.MapFrom(src => src.Subtotal + src.DeliveryMethod.Price));


            CreateMap<DeliveryMethod, DeliveryMethodDto>();


        }
    }
}
