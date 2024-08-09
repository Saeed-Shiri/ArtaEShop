

using Catalog.Core.Repositories;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Application.Commands.DeleteProduct;
public class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdCommand, bool>
{
    private readonly IProductRepository _repository;

    public DeleteProductByIdHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetProduct(request.Id);
        if(product is null)
        {
            throw new ApplicationException("There is no product with the specified id");
        }
        var response = await _repository.DeleteProduct(product.Id);
        return response;
    }
}
