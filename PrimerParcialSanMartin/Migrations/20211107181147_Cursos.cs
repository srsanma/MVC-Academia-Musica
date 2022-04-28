using Microsoft.EntityFrameworkCore.Migrations;

namespace PrimerParcialSanMartin.Migrations
{
    public partial class Cursos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    CodigoCurso = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Duracion = table.Column<int>(type: "int", nullable: false),
                    ProfesorACargo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TipoCursadaCodigo = table.Column<int>(type: "int", nullable: true),
                    FotoRuta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.CodigoCurso);
                    table.ForeignKey(
                        name: "FK_Cursos_TipoCursadas_TipoCursadaCodigo",
                        column: x => x.TipoCursadaCodigo,
                        principalTable: "TipoCursadas",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_TipoCursadaCodigo",
                table: "Cursos",
                column: "TipoCursadaCodigo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cursos");
        }
    }
}
