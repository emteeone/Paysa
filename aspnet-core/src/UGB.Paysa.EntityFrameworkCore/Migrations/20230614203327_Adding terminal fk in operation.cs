using Microsoft.EntityFrameworkCore.Migrations;

namespace UGB.Paysa.Migrations
{
    public partial class Addingterminalfkinoperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TerminalId",
                table: "UgbOperations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UgbOperations_TerminalId",
                table: "UgbOperations",
                column: "TerminalId");

            migrationBuilder.AddForeignKey(
                name: "FK_UgbOperations_UgbTerminal_TerminalId",
                table: "UgbOperations",
                column: "TerminalId",
                principalTable: "UgbTerminal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UgbOperations_UgbTerminal_TerminalId",
                table: "UgbOperations");

            migrationBuilder.DropIndex(
                name: "IX_UgbOperations_TerminalId",
                table: "UgbOperations");

            migrationBuilder.DropColumn(
                name: "TerminalId",
                table: "UgbOperations");
        }
    }
}
