using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Education.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EducationMigrationInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    CursoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.CursoId);
                });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "CursoId", "Descripcion", "FechaCreacion", "FechaPublicacion", "Precio", "Titulo" },
                values: new object[,]
                {
                    { new Guid("6c19970a-6674-455e-b081-69d8d8433f05"), "Curso de Java", new DateTime(2023, 8, 25, 16, 30, 33, 126, DateTimeKind.Local).AddTicks(1799), new DateTime(2025, 8, 25, 16, 30, 33, 126, DateTimeKind.Local).AddTicks(1799), 25m, "Master en Java Spring desde las raices" },
                    { new Guid("e81951f9-7a49-411f-ba7b-a3344ba3f443"), "Curso de Unit Test para NET Core", new DateTime(2023, 8, 25, 16, 30, 33, 126, DateTimeKind.Local).AddTicks(1806), new DateTime(2025, 8, 25, 16, 30, 33, 126, DateTimeKind.Local).AddTicks(1806), 1000m, "Master en Unit Test con CQRS" },
                    { new Guid("fbd3f80e-e3e7-4ccd-ae7a-6287447afcce"), "Curso de C# basico", new DateTime(2023, 8, 25, 16, 30, 33, 126, DateTimeKind.Local).AddTicks(1752), new DateTime(2025, 8, 25, 16, 30, 33, 126, DateTimeKind.Local).AddTicks(1764), 56m, "C# desde cero hasta avanzado." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cursos");
        }
    }
}
