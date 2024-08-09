
using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Commands.UpdateProduct;
public class UpdateProductCommand : IRequest<bool>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? Summery { get; set; }
    public string Description { get; set; }
    public string ImageFile { get; set; } = default!;
    public ProductBrand Brands { get; set; }
    public ProductType Types { get; set; }
    public decimal Price { get; set; }
}
