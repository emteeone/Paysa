using Microsoft.EntityFrameworkCore.Migrations;

namespace UGB.Paysa.Migrations
{
    public partial class Regenerated_Operation4752 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompteId",
                table: "UgbOperations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UgbOperations_CompteId",
                table: "UgbOperations",
                column: "CompteId");

            migrationBuilder.AddForeignKey(
                name: "FK_UgbOperations_UgbCompte_CompteId",
                table: "UgbOperations",
                column: "CompteId",
                principalTable: "UgbCompte",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UgbOperations_UgbCompte_CompteId",
                table: "UgbOperations");

            migrationBuilder.DropIndex(
                name: "IX_UgbOperations_CompteId",
                table: "UgbOperations");

            migrationBuilder.DropColumn(
                name: "CompteId",
                table: "UgbOperations");
        }
    }
}
