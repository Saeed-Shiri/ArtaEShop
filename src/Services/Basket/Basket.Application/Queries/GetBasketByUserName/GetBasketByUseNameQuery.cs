

using Basket.Application.Resonses;
using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Queries.GetBasketByUserName
{
    public class GetBasketByUseNameQuery : IRequest<ShoppingCartReponse>
    {
        public GetBasketByUseNameQuery(string userName)
        {
            UserName = userName;
        }
        public string UserName { get; }
    }
}
