

using Basket.Application.Responses;
using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Queries.GetBasketByUserName
{
    public sealed record GetBasketByUseNameQuery(string userName) : IRequest<ShoppingCartReponse>;

}
