

using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries.GetProductdByName;
public class GetProductsByNameQuery : IRequest<IList<ProductResponse>>
{
    public GetProductsByNameQuery(string name)
    {
        Name = name;
    }
    public string Name { get;}
}
