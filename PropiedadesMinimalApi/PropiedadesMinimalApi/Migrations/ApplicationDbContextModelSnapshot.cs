﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PropiedadesMinimalApi.Datos;

#nullable disable

namespace PropiedadesMinimalApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PropiedadesMinimalApi.Modelos.Propiedad", b =>
                {
                    b.Property<int>("IdPropiedad")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPropiedad"));

                    b.Property<bool>("Activa")
                        .HasColumnType("bit");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ubicacion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPropiedad");

                    b.ToTable("Propiedad");

                    b.HasData(
                        new
                        {
                            IdPropiedad = 1,
                            Activa = true,
                            Descripcion = "Descripción test 1",
                            FechaCreacion = new DateTime(2024, 10, 29, 11, 21, 54, 822, DateTimeKind.Local).AddTicks(850),
                            Nombre = "Casa las palmas",
                            Ubicacion = "Cartagena"
                        },
                        new
                        {
                            IdPropiedad = 2,
                            Activa = true,
                            Descripcion = "Descripción test 2",
                            FechaCreacion = new DateTime(2024, 10, 29, 11, 21, 54, 822, DateTimeKind.Local).AddTicks(861),
                            Nombre = "Casa Concorde",
                            Ubicacion = "Barranquilla"
                        },
                        new
                        {
                            IdPropiedad = 3,
                            Activa = false,
                            Descripcion = "Descripción test 3",
                            FechaCreacion = new DateTime(2024, 10, 29, 11, 21, 54, 822, DateTimeKind.Local).AddTicks(862),
                            Nombre = "Casa Centro Bogotá",
                            Ubicacion = "Bogotá"
                        },
                        new
                        {
                            IdPropiedad = 4,
                            Activa = true,
                            Descripcion = "Descripción test 4",
                            FechaCreacion = new DateTime(2024, 10, 29, 11, 21, 54, 822, DateTimeKind.Local).AddTicks(864),
                            Nombre = "Casa El Poblado",
                            Ubicacion = "Medellín"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
