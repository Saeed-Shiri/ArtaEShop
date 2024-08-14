using Basket.Application.Commands.CreateShoppingCart;
using Basket.Application.Commands.DeleteBasketByUserName;
using Basket.Application.Queries.GetBasketByUserName;
using Basket.Application.Resonses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers;


public class BasketController : ApiController
{
    private readonly IMediator _mediator;

    public BasketController(IMediator mediator)
    {
        _mediator = mediator;
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
}
