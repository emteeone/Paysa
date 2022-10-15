using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UGB.Paysa.Migrations
{
    public partial class Regenerated_Etudiant5291 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UgbEtudiant",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    INE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeEtudiant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sexe = table.Column<int>(type: "int", nullable: false),
                    DateDeNaissance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LieuDeNaissance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SituationMatrimoniale = table.Column<int>(type: "int", nullable: false),
                    CIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ville = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pays = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChambreId = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                    table.PrimaryKey("PK_UgbEtudiant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UgbEtudiant_UgbChambre_ChambreId",
                        column: x => x.ChambreId,
                        principalTable: "UgbChambre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UgbEtudiant_ChambreId",
                table: "UgbEtudiant",
                column: "ChambreId");

            migrationBuilder.CreateIndex(
                name: "IX_UgbEtudiant_TenantId",
                table: "UgbEtudiant",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UgbEtudiant");
        }
    }
}
