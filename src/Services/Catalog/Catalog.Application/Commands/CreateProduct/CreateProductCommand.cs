﻿using Catalog.Application.Responses;
using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Commands.CreateProduct;

public class CreateProductCommand : IRequest<ProductResponse>
{
    public string Name { get; set; }
    public string? Summery { get; set; }
    public string Description { get; set; }
    public string ImageFile { get; set; } = default!;
    public ProductBrand Brands { get; set; }
    public ProductType Types { get; set; }
    public decimal Price { get; set; }
}