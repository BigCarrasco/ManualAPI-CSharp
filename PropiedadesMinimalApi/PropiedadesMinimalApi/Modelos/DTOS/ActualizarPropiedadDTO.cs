namespace PropiedadesMinimalApi.Modelos.DTOS
{
    public class ActualizarPropiedadDTO
    {
        //Propiedad diseñada para crear nuevos datos desde el frente al backend
        public int IdPropiedad { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Ubicacion { get; set; }
        public bool Activa { get; set; }
        
    }
}
