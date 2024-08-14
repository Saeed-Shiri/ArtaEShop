

using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Commands.DeleteBasketByUserName
{
    public class DeleteBasketByUserNameHandler : IRequestHandler<DeleteBasketByUserNameCommand, Unit>
    {
        private readonly IBasketRepository _repository;

        public DeleteBasketByUserNameHandler(IBasketRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteBasketByUserNameCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteBasket(request.userName);
            return Unit.Value;
        }
    }
}
