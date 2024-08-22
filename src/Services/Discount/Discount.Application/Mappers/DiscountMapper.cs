

using AutoMapper;
using AutoMapper.Internal;

namespace Discount.Application.Mappers;
public static class DiscountMapper
{
    private static readonly Lazy<IMapper> _mapper = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapProperty = x => x.IsPublic() || x.GetMethod.IsAssembly;
            cfg.AddProfile<DiscountMappingProfile>();
        });

        return config.CreateMapper();
    });

    public static IMapper Mapper => _mapper.Value;
}
