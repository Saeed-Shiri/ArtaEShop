

using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Commands.DeleteDiscount;
public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, bool>
{
    private readonly IDiscountRepository _repository;

    public DeleteDiscountCommandHandler(IDiscountRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        var response = await _repository
            .DeleteDiscount(request.ProductName);
        return response;    
    }
}
