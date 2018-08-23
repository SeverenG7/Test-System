using AutoMapper;

namespace TestSystem.Logic.MapGeneric
{
    public interface IMapGeneric<TSource, TDestination>
    {
        IMapper MapperToDb { get; set; }
        IMapper MapperFromDb { get; set; }
    }
}