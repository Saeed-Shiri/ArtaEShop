

using AutoMapper;
using Ordering.Application.Features.Order.Commands.CheckoutOrder;
using Ordering.Application.Responses.Order;
using Ordering.Core.Entities;

namespace Ordering.Application.Mappers;
public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<OrderResponse, Order>().ReverseMap();
        CreateMap<CheckoutOrderCommand, Order>().ReverseMap();
    }
}
