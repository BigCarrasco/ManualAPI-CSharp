namespace PropiedadesMinimalApi.Modelos.DTOS
{
    public class PropiedadDTO
    {
        //Propiedad diseñada para devolver una respuesta despues de la creacion de una propiedad
        public int IdPropiedad { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Ubicacion { get; set; }
        public bool Activa { get; set; }
        
    }
}
