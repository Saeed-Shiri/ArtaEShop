using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;


namespace Catalog.Application.Queries.GetProductdByName;
public class GetProductsByNameHandler : IRequestHandler<GetProductsByNameQuery, IList<ProductResponse>>
{
    private readonly IProductRepository _repository;

    public GetProductsByNameHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IList<ProductResponse>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
    {
        var productList = await _repository.GetProductsByName(request.Name);
        var response = ProductMapper.Mapper.Map<IList<ProductResponse>>(productList);
        return response;
    }
}
