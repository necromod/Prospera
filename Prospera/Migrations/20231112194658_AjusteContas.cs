using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prospera.Migrations
{
    /// <inheritdoc />
    public partial class AjusteContas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PagarReceberCont",
                table: "Contas",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PagarReceberCont",
                table: "Contas");
        }
    }
}
