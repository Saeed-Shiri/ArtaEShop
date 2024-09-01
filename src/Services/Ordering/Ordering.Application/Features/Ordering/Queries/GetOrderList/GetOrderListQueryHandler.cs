

using MediatR;
using Ordering.Application.Mappers;
using Ordering.Application.Responses.Order;
using Ordering.Core.Repositories.Query;

namespace Ordering.Application.Features.Ordering.Queries.GetOrderList;
public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, IReadOnlyList<OrderResponse>>
{
    private readonly IOrderQueryRepository _orderQueryRepository;

    public GetOrderListQueryHandler(IOrderQueryRepository orderQueryRepository)
    {
        _orderQueryRepository = orderQueryRepository;
    }

    public async Task<IReadOnlyList<OrderResponse>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        var orderList = await _orderQueryRepository
            .GetAllOrdersByUsername(request.Username);
        var response = OrderMapper.Mapper.Map<IReadOnlyList<OrderResponse>>(orderList);
        return response;
    }
}
