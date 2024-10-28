namespace PropiedadesMinimalApi.Modelos.DTOS
{
    public class CrearPropiedadDTO
    {
        //Propiedad diseñada para crear nuevos datos desde el frente al backend
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Ubicacion { get; set; }
        public bool Activa { get; set; }
        
    }
}
