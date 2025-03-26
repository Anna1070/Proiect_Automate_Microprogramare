using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Colectare_pubela.Migrations
{
    /// <inheritdoc />
    public partial class RenameColectariToColectare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PubeleCetateni_Cetateni_IdCetatean",
                table: "PubeleCetateni");

            migrationBuilder.DropTable(
                name: "Colectari");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cetateni",
                table: "Cetateni");

            migrationBuilder.RenameTable(
                name: "Cetateni",
                newName: "Cetatean");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cetatean",
                table: "Cetatean",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Colectare",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false, defaultValueSql: "NEWID()"),
                    TagId = table.Column<string>(type: "TEXT", nullable: false),
                    CollectionTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colectare", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PubeleCetateni_Cetatean_IdCetatean",
                table: "PubeleCetateni",
                column: "IdCetatean",
                principalTable: "Cetatean",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PubeleCetateni_Cetatean_IdCetatean",
                table: "PubeleCetateni");

            migrationBuilder.DropTable(
                name: "Colectare");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cetatean",
                table: "Cetatean");

            migrationBuilder.RenameTable(
                name: "Cetatean",
                newName: "Cetateni");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cetateni",
                table: "Cetateni",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Colectari",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false, defaultValueSql: "NEWID()"),
                    Address = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CollectionTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TagId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colectari", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PubeleCetateni_Cetateni_IdCetatean",
                table: "PubeleCetateni",
                column: "IdCetatean",
                principalTable: "Cetateni",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
