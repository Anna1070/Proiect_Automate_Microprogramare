using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Colectare_pubela.Migrations
{
    /// <inheritdoc />
    public partial class RenameTablesBack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PubeleCetateni_Cetatean_IdCetatean",
                table: "PubeleCetateni");

            migrationBuilder.DropForeignKey(
                name: "FK_PubeleCetateni_Pubela_TagId",
                table: "PubeleCetateni");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pubela",
                table: "Pubela");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Colectare",
                table: "Colectare");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cetatean",
                table: "Cetatean");

            migrationBuilder.RenameTable(
                name: "Pubela",
                newName: "Pubele");

            migrationBuilder.RenameTable(
                name: "Colectare",
                newName: "Colectari");

            migrationBuilder.RenameTable(
                name: "Cetatean",
                newName: "Cetateni");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pubele",
                table: "Pubele",
                column: "TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colectari",
                table: "Colectari",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cetateni",
                table: "Cetateni",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PubeleCetateni_Cetateni_IdCetatean",
                table: "PubeleCetateni",
                column: "IdCetatean",
                principalTable: "Cetateni",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PubeleCetateni_Pubele_TagId",
                table: "PubeleCetateni",
                column: "TagId",
                principalTable: "Pubele",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PubeleCetateni_Cetateni_IdCetatean",
                table: "PubeleCetateni");

            migrationBuilder.DropForeignKey(
                name: "FK_PubeleCetateni_Pubele_TagId",
                table: "PubeleCetateni");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pubele",
                table: "Pubele");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Colectari",
                table: "Colectari");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cetateni",
                table: "Cetateni");

            migrationBuilder.RenameTable(
                name: "Pubele",
                newName: "Pubela");

            migrationBuilder.RenameTable(
                name: "Colectari",
                newName: "Colectare");

            migrationBuilder.RenameTable(
                name: "Cetateni",
                newName: "Cetatean");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pubela",
                table: "Pubela",
                column: "TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colectare",
                table: "Colectare",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cetatean",
                table: "Cetatean",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PubeleCetateni_Cetatean_IdCetatean",
                table: "PubeleCetateni",
                column: "IdCetatean",
                principalTable: "Cetatean",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PubeleCetateni_Pubela_TagId",
                table: "PubeleCetateni",
                column: "TagId",
                principalTable: "Pubela",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
