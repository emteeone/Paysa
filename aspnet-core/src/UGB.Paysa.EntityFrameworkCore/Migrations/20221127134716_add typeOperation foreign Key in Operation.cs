using Microsoft.EntityFrameworkCore.Migrations;

namespace UGB.Paysa.Migrations
{
    public partial class addtypeOperationforeignKeyinOperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeProductionId",
                table: "UgbOperations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UgbOperations_TypeProductionId",
                table: "UgbOperations",
                column: "TypeProductionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UgbOperations_UgbCompte_TypeProductionId",
                table: "UgbOperations",
                column: "TypeProductionId",
                principalTable: "UgbCompte",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UgbOperations_UgbCompte_TypeProductionId",
                table: "UgbOperations");

            migrationBuilder.DropIndex(
                name: "IX_UgbOperations_TypeProductionId",
                table: "UgbOperations");

            migrationBuilder.DropColumn(
                name: "TypeProductionId",
                table: "UgbOperations");
        }
    }
}
