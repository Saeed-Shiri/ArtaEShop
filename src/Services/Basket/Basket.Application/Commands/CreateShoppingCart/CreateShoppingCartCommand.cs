

using Basket.Application.Resonses;
using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Commands.CreateShoppingCart
{
    public sealed record CreateShoppingCartCommand(string userName, List<ShoppingCartItem> items) : IRequest<ShoppingCartReponse>;
}
