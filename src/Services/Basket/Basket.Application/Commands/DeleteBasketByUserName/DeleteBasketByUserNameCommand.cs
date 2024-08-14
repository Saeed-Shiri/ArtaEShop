
using MediatR;

namespace Basket.Application.Commands.DeleteBasketByUserName
{
    public sealed record DeleteBasketByUserNameCommand(string userName) : IRequest<Unit>;
}
