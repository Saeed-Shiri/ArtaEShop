using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Ordering.Commands.CheckoutOrder;
using Ordering.Application.Features.Ordering.Commands.DeleteOrder;
using Ordering.Application.Features.Ordering.Commands.UpdateOrder;
using Ordering.Application.Features.Ordering.Queries.GetOrderList;
using Ordering.Application.Responses.Order;

namespace Ordering.API.Controllers;

public class OrderController : ApiController
{
    private readonly IMediator _mediator;
    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetOrders/{username}", Name = "GetOrdersByUsername")]
    [ProducesResponseType(typeof(IReadOnlyList<OrderResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrdersByUsername(string username)
    {
        var query = new GetOrderListQuery(username);

        var response = await _mediator
            .Send(query);

        return Ok(response);
    }

    //just for testing locally as  it will be processed in queue
    [HttpPost("CheckoutOrder") ]
    [ProducesResponseType(typeof(int) , StatusCodes.Status200OK)]
    public async Task<IActionResult> CheckoutOrder([FromBody] CheckoutOrderCommand command)
    {
        var response = await _mediator
            .Send(command);

        return Ok(response);
    }

    [HttpPut("UpdateOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
    {
        await _mediator
            .Send(command);

        return NoContent();
    }


    [HttpDelete("[action]/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var command = new DeleteOrderCommand(id);
        await _mediator
            .Send(command);
        return NoContent();
    }

}
