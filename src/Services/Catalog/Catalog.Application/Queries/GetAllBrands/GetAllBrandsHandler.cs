using System.Xml;
using AutoMapper;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Queries.GetAllBrands;

public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IList<BrandResponse>>
{
    private readonly IBrandReposritory _brandReposritory;
    private readonly IMapper _mapper;
    public GetAllBrandsHandler(IBrandReposritory brandReposritory, IMapper mapper)
    {
        _brandReposritory = brandReposritory;
        _mapper = mapper;
    }

    public async Task<IList<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brandList = await _brandReposritory
            .GetAllBrands();
        var response = _mapper.Map<IList<ProductBrand>,IList<BrandResponse>>(brandList.ToList());
        return response;
    }
}