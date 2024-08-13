

using Basket.Application.Mappers;
using Basket.Application.Resonses;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Commands.CreateShoppingCart
{
    public class CreateShoppingCartHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartReponse>
    {
        private readonly IBasketRepository _repository;

        public CreateShoppingCartHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<ShoppingCartReponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            //TODO: Call discount service and apply coupons
            var shoppingCart = await _repository
                .UpdateBasket(new ShoppingCart()
                {
                    UserName = request.userName,
                    Items = request.items,
                });

            var response = BasketMapper.Mapper.Map<ShoppingCartReponse>(shoppingCart);
            return response;
        }
    }
}
