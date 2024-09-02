

using AutoMapper;
using Ordering.Application.Features.Ordering.Commands.CheckoutOrder;
using Ordering.Application.Features.Ordering.Commands.DeleteOrder;
using Ordering.Application.Features.Ordering.Commands.UpdateOrder;
using Ordering.Application.Responses.Order;
using Ordering.Core.Entities;

namespace Ordering.Application.Mappers;
public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<OrderResponse, Order>().ReverseMap();
        CreateMap<CheckoutOrderCommand, Order>().ReverseMap();
        CreateMap<UpdateOrderCommand, Order>().ReverseMap();
    }
}
