using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Colectare_pubela.Migrations
{
    /// <inheritdoc />
    public partial class Cetateni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "DataStructure",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "Cetateni",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    CNP = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cetateni", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pubela",
                columns: table => new
                {
                    TagId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pubela", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "PubeleCetateni",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TagId = table.Column<string>(type: "TEXT", nullable: false),
                    IdCetatean = table.Column<Guid>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PubeleCetateni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PubeleCetateni_Cetateni_IdCetatean",
                        column: x => x.IdCetatean,
                        principalTable: "Cetateni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PubeleCetateni_Pubela_TagId",
                        column: x => x.TagId,
                        principalTable: "Pubela",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PubeleCetateni_IdCetatean",
                table: "PubeleCetateni",
                column: "IdCetatean");

            migrationBuilder.CreateIndex(
                name: "IX_PubeleCetateni_TagId",
                table: "PubeleCetateni",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PubeleCetateni");

            migrationBuilder.DropTable(
                name: "Cetateni");

            migrationBuilder.DropTable(
                name: "Pubela");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "DataStructure",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldDefaultValueSql: "NEWID()");
        }
    }
}
