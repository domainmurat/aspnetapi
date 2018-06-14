using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInfo.Data.Mapping
{
    public static class MapperExtension
    {
        public static IList<TDestination> MapTo<TSource, TDestination>(this IList<TSource> source, Action<IMapperConfiguration> action)
        {
            MapperConfiguration config = new MapperConfiguration(action);

            IMapper mapper = config.CreateMapper();
            return mapper.Map<IList<TSource>, IList<TDestination>>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, Action<IMapperConfiguration> action)
        {
            MapperConfiguration config = new MapperConfiguration(action);

            IMapper mapper = config.CreateMapper();
            return mapper.Map<TSource, TDestination>(source);
        }

        public static IList<TDestination> MapTo<TSource, TDestination>(this IList<TSource> source)
        {
            Mapper.CreateMap<TSource, TDestination>();
            return Mapper.Map<IList<TSource>, IList<TDestination>>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            Mapper.CreateMap<TSource, TDestination>();
            return Mapper.Map<TSource, TDestination>(source);
        }
    }
}
