using AutoMapper;
using Catalog.Application.Queries.GetAllBrands;
using Catalog.Application.Queries.GetAllTypes;
using Catalog.Core.Entities;

namespace Catalog.Application.Mappers;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<ProductBrand, BrandResponse>().ReverseMap();
        CreateMap<ProductType, TypeResponse>().ReverseMap();
    }
}