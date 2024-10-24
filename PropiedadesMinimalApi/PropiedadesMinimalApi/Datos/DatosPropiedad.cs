using PropiedadesMinimalApi.Modelos;

namespace PropiedadesMinimalApi.Datos
{
    public static class DatosPropiedad
    {
        public static List<Propiedad> listaPropiedades = new List<Propiedad>
        {
            new Propiedad{IdPropiedad=1, Nombre="Casa Lorena", Descripcion="Linda casa en queretaro", Ubicacion="Queretaro", Activa=true, FechaCreacion=DateTime.Now.AddDays(-10)},
            new Propiedad{IdPropiedad=2, Nombre="Casa las palmas", Descripcion="Linda casa en Manzanillo", Ubicacion="Manzanillo", Activa=true, FechaCreacion=DateTime.Now.AddDays(-15)},
            new Propiedad{IdPropiedad=3, Nombre="Casa la villa", Descripcion="Linda casa en Colima", Ubicacion="Colima", Activa=true, FechaCreacion=DateTime.Now.AddDays(-30)},
            new Propiedad{IdPropiedad=4, Nombre="Casa Cedro", Descripcion="Linda casa en Manzanillo", Ubicacion="Manzanillo", Activa=true, FechaCreacion=DateTime.Now.AddDays(-12)},

        };
    }
}
