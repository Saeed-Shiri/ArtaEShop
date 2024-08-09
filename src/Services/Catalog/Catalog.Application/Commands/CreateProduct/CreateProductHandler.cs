

using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Commands.CreateProduct;
public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    private readonly IProductRepository _repository;

    public CreateProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var newProduct = ProductMapper.Mapper.Map<Product>(request);      
        if (newProduct is null)
        {
            throw new ApplicationException("There is an issue with mapping while creating new product");
        }

        var addedProduct = await _repository.CreateProduct(newProduct);
        var response = ProductMapper.Mapper.Map<ProductResponse>(addedProduct);
        return response;
    }
}
