using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Colectare_pubela.Migrations
{
    /// <inheritdoc />
    public partial class Colectari : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataStructure");

            migrationBuilder.CreateTable(
                name: "Colectari",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false, defaultValueSql: "NEWID()"),
                    TagId = table.Column<string>(type: "TEXT", nullable: false),
                    CollectionTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colectari", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colectari");

            migrationBuilder.CreateTable(
                name: "DataStructure",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false, defaultValueSql: "NEWID()"),
                    CollectionTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TagId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataStructure", x => x.Id);
                });
        }
    }
}
