

using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Exceptions;
using Ordering.Core.Entities;
using Ordering.Core.Repositories.Commnad;
using Ordering.Core.Repositories.Query;

namespace Ordering.Application.Features.Ordering.Commands.DeleteOrder;
public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderCommandRepository _orderCommandRepository;
    private readonly IOrderQueryRepository _orderQueryRepository;
    private readonly ILogger<DeleteOrderCommandHandler> _logger;  

    public DeleteOrderCommandHandler(IOrderCommandRepository orderCommandRepository, IOrderQueryRepository orderQueryRepository, ILogger<DeleteOrderCommandHandler> logger)
    {
        _orderCommandRepository = orderCommandRepository;
        _orderQueryRepository = orderQueryRepository;
        _logger = logger;
    }

    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToDelete = await _orderQueryRepository
            .GetByIdAsync(request.Id);
        if (orderToDelete == null) {
            throw new OrderNotFoundException(nameof(Order), request.Id);
        }

        await _orderCommandRepository
            .DeleteAsync(orderToDelete);

        _logger.LogInformation($"Order with Id: {orderToDelete.Id} was deleted successfully.");
    }
}
