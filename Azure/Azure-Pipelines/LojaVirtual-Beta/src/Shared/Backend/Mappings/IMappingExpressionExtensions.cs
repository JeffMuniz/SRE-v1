using System.Collections.Generic;
using System.Linq;

namespace AutoMapper
{
    public static class IMappingExpressionExtensions
    {
        public static TDestination Map<TDestination>(this IMapper mapper, params object[] sources)
        {
            var destination = mapper.Map<TDestination>(sources.First());

            destination = mapper.Map<TDestination>(destination, sources.Skip(1));

            return destination;
        }

        public static TDestination Map<TDestination>(this IMapper mapper, TDestination destination, params object[] sources) =>
            Map(mapper, destination, sources as IEnumerable<object>);

        private static TDestination Map<TDestination>(this IMapper mapper, TDestination destination, IEnumerable<object> sources)
        {
            foreach (var source in sources)
                destination = mapper.Map(source, destination);

            return destination;
        }

        public static IMappingExpression<TSource, TDest> IgnoreForAllOtherMembers<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
        {
            expression.ForAllOtherMembers(opt => opt.Ignore());

            return expression;
        }
    }
}
