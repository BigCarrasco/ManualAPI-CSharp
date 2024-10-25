using AutoMapper;
using PropiedadesMinimalApi.Modelos;
using PropiedadesMinimalApi.Modelos.DTOS;

namespace PropiedadesMinimalApi.Mapas
{
    public class ConfiguracionMapas : Profile //extiende de automapper  
    {
        public ConfiguracionMapas()
        {
            CreateMap<Propiedad, CrearPropiedadDTO>().ReverseMap();
            CreateMap<Propiedad, PropiedadDTO>().ReverseMap();
        }
    }
}
