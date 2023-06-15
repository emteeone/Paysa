using Microsoft.EntityFrameworkCore.Migrations;

namespace UGB.Paysa.Migrations
{
    public partial class AddingCarteforeignKeyInOperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarteId",
                table: "UgbOperations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UgbOperations_CarteId",
                table: "UgbOperations",
                column: "CarteId");

            migrationBuilder.AddForeignKey(
                name: "FK_UgbOperations_UgbCarte_CarteId",
                table: "UgbOperations",
                column: "CarteId",
                principalTable: "UgbCarte",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UgbOperations_UgbCarte_CarteId",
                table: "UgbOperations");

            migrationBuilder.DropIndex(
                name: "IX_UgbOperations_CarteId",
                table: "UgbOperations");

            migrationBuilder.DropColumn(
                name: "CarteId",
                table: "UgbOperations");
        }
    }
}
