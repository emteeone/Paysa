using Microsoft.EntityFrameworkCore.Migrations;

namespace UGB.Paysa.Migrations
{
    public partial class Regenerated_Etudiant9906 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "UgbEtudiant",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UgbEtudiant_UserId",
                table: "UgbEtudiant",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UgbEtudiant_AbpUsers_UserId",
                table: "UgbEtudiant",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UgbEtudiant_AbpUsers_UserId",
                table: "UgbEtudiant");

            migrationBuilder.DropIndex(
                name: "IX_UgbEtudiant_UserId",
                table: "UgbEtudiant");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UgbEtudiant");
        }
    }
}
