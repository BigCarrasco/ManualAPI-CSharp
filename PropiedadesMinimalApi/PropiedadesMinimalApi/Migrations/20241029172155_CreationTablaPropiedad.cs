using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PropiedadesMinimalApi.Migrations
{
    /// <inheritdoc />
    public partial class CreationTablaPropiedad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Propiedad",
                columns: table => new
                {
                    IdPropiedad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activa = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propiedad", x => x.IdPropiedad);
                });

            migrationBuilder.InsertData(
                table: "Propiedad",
                columns: new[] { "IdPropiedad", "Activa", "Descripcion", "FechaCreacion", "Nombre", "Ubicacion" },
                values: new object[,]
                {
                    { 1, true, "Descripción test 1", new DateTime(2024, 10, 29, 11, 21, 54, 822, DateTimeKind.Local).AddTicks(850), "Casa las palmas", "Cartagena" },
                    { 2, true, "Descripción test 2", new DateTime(2024, 10, 29, 11, 21, 54, 822, DateTimeKind.Local).AddTicks(861), "Casa Concorde", "Barranquilla" },
                    { 3, false, "Descripción test 3", new DateTime(2024, 10, 29, 11, 21, 54, 822, DateTimeKind.Local).AddTicks(862), "Casa Centro Bogotá", "Bogotá" },
                    { 4, true, "Descripción test 4", new DateTime(2024, 10, 29, 11, 21, 54, 822, DateTimeKind.Local).AddTicks(864), "Casa El Poblado", "Medellín" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Propiedad");
        }
    }
}
