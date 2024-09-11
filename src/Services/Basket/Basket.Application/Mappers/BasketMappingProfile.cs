

using AutoMapper;
using Basket.Application.Responses;
using Basket.Core.Entities;
using EventBus.Messages.Events;

namespace Basket.Application.Mappers
{
    public class BasketMappingProfile : Profile
    {
        public BasketMappingProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartReponse>().ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingCartItemResonse>().ReverseMap();
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
