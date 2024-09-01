

using MediatR;
using Ordering.Application.Responses.Order;

namespace Ordering.Application.Features.Ordering.Queries.GetOrderList;
public sealed record GetOrderListQuery(string Username) : IRequest<IReadOnlyList<OrderResponse>>;
