using System.Xml;
using AutoMapper;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Queries.GetAllBrands;

public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IList<BrandResponse>>
{
    private readonly IBrandReposritory _brandReposritory;
    public GetAllBrandsHandler(IBrandReposritory brandReposritory)
    {
        _brandReposritory = brandReposritory;
    }

    public async Task<IList<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brandList = await _brandReposritory
            .GetAllBrands();
        var response = ProductMapper.Mapper.Map<IList<BrandResponse>>(brandList);
        return response;
    }
}