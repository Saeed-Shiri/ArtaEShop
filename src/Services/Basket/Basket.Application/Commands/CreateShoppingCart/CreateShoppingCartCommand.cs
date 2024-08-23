

using Basket.Application.Responses;
using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Commands.CreateShoppingCart
{
    public sealed record CreateShoppingCartCommand(string UserName, List<ShoppingCartItem> Items) : IRequest<ShoppingCartReponse>;
}
