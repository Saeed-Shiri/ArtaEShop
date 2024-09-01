

using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Mappers;
using Ordering.Core.Entities;
using Ordering.Core.Repositories.Commnad;

namespace Ordering.Application.Features.Ordering.Commands.CheckoutOrder;
public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
{
    private readonly IOrderCommandRepository _orderCommandRepository;
    private readonly ILogger _logger;
    public CheckoutOrderCommandHandler(IOrderCommandRepository orderCommandRepository, ILogger logger)
    {
        _orderCommandRepository = orderCommandRepository;
        _logger = logger;
    }

    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = OrderMapper.Mapper.Map<Core.Entities.Order>(request);
        var createdOrder = await _orderCommandRepository
            .AddAsync(orderEntity);
        _logger.LogInformation($"Order {createdOrder} successfully created.");
        return createdOrder.Id;
    }
}
