

using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Queries.GetProductsByBrand;
public class GetProductsByBrandHandler : IRequestHandler<GetProductsByBrandQuery, IList<ProductResponse>>

{
    private readonly IProductRepository _repository;

    public GetProductsByBrandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IList<ProductResponse>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
    {
        var productList = await _repository.GetProductsByBrand(request.BrandName);
        var response = ProductMapper.Mapper.Map<IList<ProductResponse>>(productList);
        return response;
    }
}
