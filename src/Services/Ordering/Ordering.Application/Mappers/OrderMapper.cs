using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;

namespace Ordering.Application.Mappers;
public static class OrderMapper
{
    private static readonly Lazy<IMapper> _mapper = new Lazy<IMapper>(() =>
    {
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapProperty = x => x.IsPublic() || x.GetMethod.IsAssembly;
            cfg.AddProfile<OrderMappingProfile>();
        });

        var mapper = config.CreateMapper();
        return mapper;
    });

    public static IMapper Mapper => _mapper.Value;
}
