using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporadaDeBasquete.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Temporada",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TemporadaDescricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temporada", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistroJogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Placar = table.Column<int>(nullable: false),
                    DataJogo = table.Column<DateTime>(nullable: false),
                    MinimoTemporada = table.Column<int>(nullable: true),
                    MaximoTemporada = table.Column<int>(nullable: true),
                    QuebraRecordeMaximo = table.Column<int>(nullable: true),
                    QuebraRecordeMinimo = table.Column<int>(nullable: true),
                    TemporadaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroJogo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistroJogo_Temporada_TemporadaId",
                        column: x => x.TemporadaId,
                        principalTable: "Temporada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistroJogo_TemporadaId",
                table: "RegistroJogo",
                column: "TemporadaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistroJogo");

            migrationBuilder.DropTable(
                name: "Temporada");
        }
    }
}
