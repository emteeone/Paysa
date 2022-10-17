using Microsoft.EntityFrameworkCore.Migrations;

namespace UGB.Paysa.Migrations
{
    public partial class Regenerated_Terminal7974 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Matricule",
                table: "UgbTerminal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "UgbTerminal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Matricule",
                table: "UgbTerminal");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "UgbTerminal");
        }
    }
}
