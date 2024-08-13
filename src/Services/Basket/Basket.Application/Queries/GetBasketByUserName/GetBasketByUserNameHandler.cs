

using Basket.Application.Mappers;
using Basket.Application.Resonses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Queries.GetBasketByUserName
{
    public class GetBasketByUserNameHandler : IRequestHandler<GetBasketByUseNameQuery, ShoppingCartReponse>
    {
        private readonly IBasketRepository _repository;

        public GetBasketByUserNameHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<ShoppingCartReponse> Handle(GetBasketByUseNameQuery request, CancellationToken cancellationToken)
        {
            var cart = await _repository
                .GetBasket(request.UserName);

            var response = BasketMapper.Mapper.Map<ShoppingCartReponse>(cart);
            return response;
        }
    }
}
