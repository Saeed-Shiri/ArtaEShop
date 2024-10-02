

using System.Runtime.InteropServices;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Features.Ordering.Commands.CheckoutOrder;
using Ordering.Application.Mappers;

namespace Ordering.Infrastructure.EventBusConsumer;

//iConsumer Implementation
public class BasketOrderingConsumer : IConsumer<BasketCheckoutEvent>
{
    private readonly ISender _sender;
    private readonly ILogger<BasketOrderingConsumer> _logger;

    public BasketOrderingConsumer(
        ISender sender,
        ILogger<BasketOrderingConsumer> logger)
    {
        _sender = sender;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        var command = OrderMapper.Mapper.Map<CheckoutOrderCommand>(context.Message);

        var result = await _sender.Send(command);

        _logger.LogInformation($"Besket checkout event coompleted!!!");
    }
}
