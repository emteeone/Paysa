using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UGB.Paysa.Migrations
{
    public partial class Regenerated_PaiementLoyer1250 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UgbPaiementLoyer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Mois = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Annee = table.Column<int>(type: "int", nullable: false),
                    CodePaiement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChambreId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OperationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UgbPaiementLoyer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UgbPaiementLoyer_UgbChambre_ChambreId",
                        column: x => x.ChambreId,
                        principalTable: "UgbChambre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UgbPaiementLoyer_UgbOperations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "UgbOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UgbPaiementLoyer_ChambreId",
                table: "UgbPaiementLoyer",
                column: "ChambreId");

            migrationBuilder.CreateIndex(
                name: "IX_UgbPaiementLoyer_OperationId",
                table: "UgbPaiementLoyer",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_UgbPaiementLoyer_TenantId",
                table: "UgbPaiementLoyer",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UgbPaiementLoyer");
        }
    }
}
