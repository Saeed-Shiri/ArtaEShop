﻿using AutoMapper;
using Catalog.Application.Commands.CreateProduct;
using Catalog.Application.Commands.UpdateProduct;
using Catalog.Application.Responses;
using Catalog.Core.Entities;

namespace Catalog.Application.Mappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<ProductBrand, BrandResponse>().ReverseMap();
        CreateMap<ProductType, TypeResponse>().ReverseMap();
        CreateMap<Product, ProductResponse>().ReverseMap();
        CreateMap<Product, UpdateProductCommand>().ReverseMap();
        CreateMap<Product, CreateProductCommand>().ReverseMap();
    }
}