using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prospera.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeUsuario = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    EmailUsuario = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    SenhaUsuario = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CargoUsuario = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    DatCadastroUsuario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatUltimoAcesUsuario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusUsuario = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Contas",
                columns: table => new
                {
                    IdContas = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoCont = table.Column<int>(type: "int", nullable: false),
                    TipoCont = table.Column<int>(type: "int", nullable: false),
                    DatEmissaoCont = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatVenciCont = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DevedorCont = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    PagadorCont = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Descricaocont = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ValorCont = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StatusCont = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MetodoPgtoCont = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ObservacaoCont = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contas", x => x.IdContas);
                    table.ForeignKey(
                        name: "FK_Contas_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Extrato",
                columns: table => new
                {
                    IdExtrato = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeExtrato = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ValorExtrato = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DestinatarioExtrato = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    RemetenteExtrato = table.Column<int>(type: "int", maxLength: 120, nullable: false),
                    DataExtrato = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusExtrato = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ObservacaoExtrato = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extrato", x => x.IdExtrato);
                    table.ForeignKey(
                        name: "FK_Extrato_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Terceiros",
                columns: table => new
                {
                    IdTerceiros = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeTerceiros = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    TelefoneTerceiros = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Telefone2Terceiros = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmailTerceiros = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    EnderecoTerceiros = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CidadeTerceiros = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    BairroTerceiros = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    UFTerceiros = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CEPTerceiros = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ObservacaoTerceiros = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    DataCadastroTerceiros = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUltimaMovimentacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusTerceiros = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terceiros", x => x.IdTerceiros);
                    table.ForeignKey(
                        name: "FK_Terceiros_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContaBancaria",
                columns: table => new
                {
                    IdContaBancaria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitularContBan = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    NumContBan = table.Column<int>(type: "int", nullable: false),
                    AgenciaContBan = table.Column<int>(type: "int", nullable: false),
                    TipoContBan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SaldoContBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ObsContBan = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdTerceiros = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaBancaria", x => x.IdContaBancaria);
                    table.ForeignKey(
                        name: "FK_ContaBancaria_Terceiros_IdTerceiros",
                        column: x => x.IdTerceiros,
                        principalTable: "Terceiros",
                        principalColumn: "IdTerceiros");
                    table.ForeignKey(
                        name: "FK_ContaBancaria_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContaBancaria_IdTerceiros",
                table: "ContaBancaria",
                column: "IdTerceiros");

            migrationBuilder.CreateIndex(
                name: "IX_ContaBancaria_IdUsuario",
                table: "ContaBancaria",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Contas_IdUsuario",
                table: "Contas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Extrato_IdUsuario",
                table: "Extrato",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Terceiros_IdUsuario",
                table: "Terceiros",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContaBancaria");

            migrationBuilder.DropTable(
                name: "Contas");

            migrationBuilder.DropTable(
                name: "Extrato");

            migrationBuilder.DropTable(
                name: "Terceiros");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
