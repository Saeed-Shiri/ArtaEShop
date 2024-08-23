

using Basket.Application.GrpcServices;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Commands.CreateShoppingCart
{
    public class CreateShoppingCartHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartReponse>
    {
        private readonly IBasketRepository _repository;
        private readonly DiscountGrpcService _discountGrpcService;

        public CreateShoppingCartHandler(IBasketRepository repository, DiscountGrpcService discountGrpcService)
        {
            _repository = repository;
            _discountGrpcService = discountGrpcService;
        }

        public async Task<ShoppingCartReponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            /*apply discount service for each items in shopoing cart*/
            foreach (var item in request.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            var shoppingCart = await _repository
                .UpdateBasket(new ShoppingCart()
                {
                    UserName = request.UserName,
                    Items = request.Items,
                });

            var response = BasketMapper.Mapper.Map<ShoppingCartReponse>(shoppingCart);
            return response;
        }
    }
}
