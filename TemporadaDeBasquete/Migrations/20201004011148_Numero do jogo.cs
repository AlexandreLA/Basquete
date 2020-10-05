using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporadaDeBasquete.Migrations
{
    public partial class Numerodojogo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumeroJogo",
                table: "RegistroJogo",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroJogo",
                table: "RegistroJogo");
        }
    }
}
