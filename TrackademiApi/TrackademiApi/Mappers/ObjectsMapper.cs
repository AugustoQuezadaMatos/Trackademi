using System;
namespace TrackademiApi.Mappers
{

    public class ObjectsMapper<TSource, TTarget>
    {
        private readonly Func<TSource, TTarget> _map;

        public ObjectsMapper(Func<TSource, TTarget> map)
        {
            _map = map;
        }

        public TTarget Map(TSource source)
        {
            return _map(source);
        }
    }
}
