using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Queries.GetAllBrands;

public class GetAllBrandsQuery : IRequest<IList<BrandResponse>>
{
    
}