using Microsoft.EntityFrameworkCore.Migrations;

namespace UGB.Paysa.Migrations
{
    public partial class addNewPropertiesInCarte_IsBlocked_IsPersonnalized : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "UgbCarte",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPersonnalized",
                table: "UgbCarte",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "UgbCarte");

            migrationBuilder.DropColumn(
                name: "IsPersonnalized",
                table: "UgbCarte");
        }
    }
}
