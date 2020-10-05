using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TemporadaDeBasquete.Migrations
{
    public partial class removerdatadojogo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataJogo",
                table: "RegistroJogo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataJogo",
                table: "RegistroJogo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
