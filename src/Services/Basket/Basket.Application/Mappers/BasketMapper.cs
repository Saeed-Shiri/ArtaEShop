

using AutoMapper;
using AutoMapper.Internal;

namespace Basket.Application.Mappers
{
    public static class BasketMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.IsPublic() || p.GetMethod.IsAssembly;
                cfg.AddProfile<BasketMappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => lazy.Value;
    }
}
