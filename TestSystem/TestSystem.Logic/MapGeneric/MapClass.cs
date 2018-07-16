using AutoMapper;

namespace TestSystem.Logic.MapGeneric
{
    public class MapClass<TModel, TModelDTO> 
    {
        protected dynamic MapperTo { get; set; }
        protected dynamic MapperFrom { get; set; }

        public MapClass()
        {
            MapperFrom = new MapperConfiguration
                (mcf => mcf.CreateMap(typeof(TModel), typeof(TModelDTO))).CreateMapper();
            MapperTo = new MapperConfiguration
                (mcf => mcf.CreateMap(typeof(TModelDTO), typeof(TModel))).CreateMapper();
        }

        
    }
}
