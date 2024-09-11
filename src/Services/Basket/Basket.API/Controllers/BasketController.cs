using Basket.Application.Commands.CreateShoppingCart;
using Basket.Application.Commands.DeleteBasketByUserName;
using Basket.Application.Mappers;
using Basket.Application.Queries.GetBasketByUserName;
using Basket.Application.Responses;
using Basket.Core.Entities;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers;


public class BasketController : ApiController
{
    private readonly IMediator _mediator;
    private IPublishEndpoint _publishEndpoint;

    public BasketController(IMediator mediator, IPublishEndpoint publishEndpoint)
    {
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet("[action]/{userName}", Name ="GetBasketByUserName")]
    [ProducesResponseType(typeof(ShoppingCartReponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBasket(string userName)
    {
        var query = new GetBasketByUseNameQuery(userName);
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpPost("CreateBasket")]
    [ProducesResponseType(typeof(ShoppingCartReponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateBasket([FromBody] CreateShoppingCartCommand request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete("[action]/{userName}", Name = "DeleteBasketByUserName")]
    [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteBasket(string userName)
    {
        var query = new DeleteBasketByUserNameCommand(userName);
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpPost("Checkout")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
    {
        //get existing basket with username
        var query = new GetBasketByUseNameQuery(basketCheckout.UserName);

        var basket = await _mediator
            .Send(query);

        if(basket == null)
        {
            return BadRequest();
        }

        var eventMessage = BasketMapper.Mapper.Map<BasketCheckoutEvent>(basketCheckout);
        eventMessage.TotalPrice = basket.TotalPrice;

        await _publishEndpoint
            .Publish(eventMessage);

        //remove basket
        var deleteQuery = new DeleteBasketByUserNameCommand(basketCheckout.UserName);

        await _mediator
            .Send(deleteQuery);

        return Accepted();
    }

}
