using Arizona.APIs.DTOs;
using Arizona.Core.Entities;
using Arizona.Core.Entities.Identity;
using Arizona.Core.Entities.OrderAggregate;
using AutoMapper;

namespace Arizona.APIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Brand, O => O.MapFrom(S => S.Brand.Name))
                .ForMember(d => d.Category, O => O.MapFrom(S => S.Category.Name))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<OrderAddressDto, OrderAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, O => O.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost , O => O.MapFrom(s => s.DeliveryMethod.Cost));

            CreateMap<OrderItems, OrderItemsDto>()
                .ForMember(d => d.ProductName, O => O.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.ProductId, O => O.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.PictureUrl, O => O.MapFrom(s => s.Product.PictureUrl))
                .ForMember(d => d.PictureUrl , O => O.MapFrom<OrderItemPictureUrlResolver>());

        }
    }
}
