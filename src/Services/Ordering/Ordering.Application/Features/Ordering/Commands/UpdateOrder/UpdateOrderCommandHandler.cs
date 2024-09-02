

using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Exceptions;
using Ordering.Application.Mappers;
using Ordering.Core.Entities;
using Ordering.Core.Repositories.Commnad;
using Ordering.Core.Repositories.Query;

namespace Ordering.Application.Features.Ordering.Commands.UpdateOrder;
public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IOrderCommandRepository _orderCommandRepisitory;
    private readonly IOrderQueryRepository _orderQueryRepisitory;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;

    public UpdateOrderCommandHandler(IOrderCommandRepository orderCommandRepisitory,
        IOrderQueryRepository orderQueryRepisitory,
        ILogger<UpdateOrderCommandHandler> logger)
    {
        _orderCommandRepisitory = orderCommandRepisitory;
        _orderQueryRepisitory = orderQueryRepisitory;
        _logger = logger;
    }

    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToUpdate = await _orderQueryRepisitory
            .GetByIdAsync(request.Id, cancellationToken);

        if (orderToUpdate == null) 
        {
            throw new OrderNotFoundException(nameof(Order), request.Id);
        }

        OrderMapper.Mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));
        await _orderCommandRepisitory
            .UpdateAsync(orderToUpdate, cancellationToken);

        _logger.LogInformation($"Order with Id: {orderToUpdate.Id} is successfully updated");
    }

}
