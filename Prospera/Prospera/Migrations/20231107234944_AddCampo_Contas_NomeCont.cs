using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prospera.Migrations
{
    /// <inheritdoc />
    public partial class AddCampo_Contas_NomeCont : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeCont",
                table: "Contas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeCont",
                table: "Contas");
        }
    }
}
