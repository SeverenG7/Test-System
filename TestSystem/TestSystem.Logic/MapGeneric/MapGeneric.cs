using AutoMapper;

namespace TestSystem.Logic.MapGeneric
{
    public class MapClass<TModel, TModelDTO>
    {
        protected IMapper MapperToDB;
        protected IMapper MapperFromDB;

        public MapClass()
        {
            MapperFromDB = new MapperConfiguration
                (mcf => mcf.CreateMap(typeof(TModel), typeof(TModelDTO))).CreateMapper();
            MapperToDB = new MapperConfiguration
                (mcf => mcf.CreateMap(typeof(TModelDTO), typeof(TModel))).CreateMapper();
        }
    }
}