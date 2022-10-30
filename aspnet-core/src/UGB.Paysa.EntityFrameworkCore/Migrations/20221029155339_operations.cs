using Microsoft.EntityFrameworkCore.Migrations;

namespace UGB.Paysa.Migrations
{
    public partial class operations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Operations",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Operations");

            migrationBuilder.RenameTable(
                name: "Operations",
                newName: "UgbOperations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UgbOperations",
                table: "UgbOperations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UgbOperations_TenantId",
                table: "UgbOperations",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UgbOperations",
                table: "UgbOperations");

            migrationBuilder.DropIndex(
                name: "IX_UgbOperations_TenantId",
                table: "UgbOperations");

            migrationBuilder.RenameTable(
                name: "UgbOperations",
                newName: "Operations");

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "Operations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Operations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "Operations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Operations",
                table: "Operations",
                column: "Id");
        }
    }
}
