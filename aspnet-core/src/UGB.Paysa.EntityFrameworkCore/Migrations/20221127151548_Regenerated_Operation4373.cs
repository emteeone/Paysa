using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UGB.Paysa.Migrations
{
    public partial class Regenerated_Operation4373 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UgbOperations_UgbTypeOperation_TypeOperationId",
                table: "UgbOperations");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "UgbOperations");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "UgbOperations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UgbOperations");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
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
                name: "FK_UgbOperations_UgbTypeOperation_TypeProductionId",
                table: "UgbOperations",
                column: "TypeProductionId",
                principalTable: "UgbTypeOperation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UgbOperations_UgbTypeOperation_TypeProductionId",
                table: "UgbOperations");

            migrationBuilder.RenameColumn(
                name: "TypeProductionId",
                table: "UgbOperations",
                newName: "TypeOperationId");

            migrationBuilder.RenameIndex(
                name: "IX_UgbOperations_TypeProductionId",
                table: "UgbOperations",
                newName: "IX_UgbOperations_TypeOperationId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "UgbOperations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "UgbOperations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UgbOperations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "UgbOperations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UgbOperations_UgbTypeOperation_TypeOperationId",
                table: "UgbOperations",
                column: "TypeOperationId",
                principalTable: "UgbTypeOperation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
