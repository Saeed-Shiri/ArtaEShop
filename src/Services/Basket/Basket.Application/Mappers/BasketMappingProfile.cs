﻿

using AutoMapper;
using Basket.Application.Resonses;
using Basket.Core.Entities;

namespace Basket.Application.Mappers
{
    public class BasketMappingProfile : Profile
    {
        public BasketMappingProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartReponse>().ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingCartItemResonse>().ReverseMap();
        }
    }
}
