using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prospera.Migrations
{
    /// <inheritdoc />
    public partial class ajust_ContaBancaria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodigoContaBanc",
                table: "ContaBancaria",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoContaBanc",
                table: "ContaBancaria");
        }
    }
}
