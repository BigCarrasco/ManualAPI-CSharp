using Microsoft.EntityFrameworkCore;
using PropiedadesMinimalApi.Modelos;

namespace PropiedadesMinimalApi.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Propiedad> Propiedad { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Propiedad>().HasData(
                new Propiedad
                {
                    IdPropiedad = 1,
                    Nombre = "Casa las palmas",
                    Descripcion = "Descripción test 1",
                    Ubicacion = "Cartagena",
                    Activa = true,
                    FechaCreacion = DateTime.Now
                },
                new Propiedad
                {
                    IdPropiedad = 2,
                    Nombre = "Casa Concorde",
                    Descripcion = "Descripción test 2",
                    Ubicacion = "Barranquilla",
                    Activa = true,
                    FechaCreacion = DateTime.Now
                },
                new Propiedad
                {
                    IdPropiedad = 3,
                    Nombre = "Casa Centro Bogotá",
                    Descripcion = "Descripción test 3",
                    Ubicacion = "Bogotá",
                    Activa = false,
                    FechaCreacion = DateTime.Now
                },
                new Propiedad
                {
                    IdPropiedad = 4,
                    Nombre = "Casa El Poblado",
                    Descripcion = "Descripción test 4",
                    Ubicacion = "Medellín",
                    Activa = true,
                    FechaCreacion = DateTime.Now
                });

        }
    }
}
