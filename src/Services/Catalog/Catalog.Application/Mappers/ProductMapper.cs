using AutoMapper;
using AutoMapper.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Mappers;
public static class ProductMapper
{
    private static readonly Lazy<IMapper> mapper = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapProperty = x => x.GetMethod.IsPublic || x.GetMethod.IsAssembly;
            cfg.AddProfile<ProductMappingProfile>();
        }

        );

        return config.CreateMapper();
    });

    public static IMapper Mapper => mapper.Value;
}
