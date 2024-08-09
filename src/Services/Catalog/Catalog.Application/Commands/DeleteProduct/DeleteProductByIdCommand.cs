
using MediatR;

namespace Catalog.Application.Commands.DeleteProduct;
public class DeleteProductByIdCommand : IRequest<bool>
{
    public DeleteProductByIdCommand(string id)
    {
        Id = id;
    }
    public string Id { get;}
}
