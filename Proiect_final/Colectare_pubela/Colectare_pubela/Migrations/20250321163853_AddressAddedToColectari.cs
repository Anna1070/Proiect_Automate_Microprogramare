using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Colectare_pubela.Migrations
{
    /// <inheritdoc />
    public partial class AddressAddedToColectari : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Colectari",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Colectari");
        }
    }
}
