using Microsoft.EntityFrameworkCore.Migrations;

namespace UGB.Paysa.Migrations
{
    public partial class addtypeOperationforeignKeyinOperationnew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UgbOperations_UgbCompte_TypeProductionId",
                table: "UgbOperations");

            migrationBuilder.RenameColumn(
                name: "TypeProductionId",
                table: "UgbOperations",
                newName: "TypeOperationId");

            migrationBuilder.RenameIndex(
                name: "IX_UgbOperations_TypeProductionId",
                table: "UgbOperations",
                newName: "IX_UgbOperations_TypeOperationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UgbOperations_UgbTypeOperation_TypeOperationId",
                table: "UgbOperations",
                column: "TypeOperationId",
                principalTable: "UgbTypeOperation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UgbOperations_UgbTypeOperation_TypeOperationId",
                table: "UgbOperations");

            migrationBuilder.RenameColumn(
                name: "TypeOperationId",
                table: "UgbOperations",
                newName: "TypeProductionId");

            migrationBuilder.RenameIndex(
                name: "IX_UgbOperations_TypeOperationId",
                table: "UgbOperations",
                newName: "IX_UgbOperations_TypeProductionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UgbOperations_UgbCompte_TypeProductionId",
                table: "UgbOperations",
                column: "TypeProductionId",
                principalTable: "UgbCompte",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
