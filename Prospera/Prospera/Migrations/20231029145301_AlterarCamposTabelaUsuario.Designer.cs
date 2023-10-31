﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Prospera.Data;

#nullable disable

namespace Prospera.Migrations
{
    [DbContext(typeof(ProsperaContext))]
    [Migration("20231029145301_AlterarCamposTabelaUsuario")]
    partial class AlterarCamposTabelaUsuario
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Prospera.Models.ContaBancaria", b =>
                {
                    b.Property<int>("IdContaBancaria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdContaBancaria"));

                    b.Property<int>("AgenciaContBan")
                        .HasColumnType("int");

                    b.Property<int>("IdTerceiros")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<int>("NumContBan")
                        .HasColumnType("int");

                    b.Property<string>("ObsContBan")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<decimal>("SaldoContBan")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("TipoContBan")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TitularContBan")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("IdContaBancaria");

                    b.HasIndex("IdTerceiros");

                    b.HasIndex("IdUsuario");

                    b.ToTable("ContaBancaria");
                });

            modelBuilder.Entity("Prospera.Models.Contas", b =>
                {
                    b.Property<int>("IdContas")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdContas"));

                    b.Property<int>("CodigoCont")
                        .HasColumnType("int");

                    b.Property<DateTime>("DatEmissaoCont")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatVenciCont")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricaocont")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("DevedorCont")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("MetodoPgtoCont")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ObservacaoCont")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("PagadorCont")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("StatusCont")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("TipoCont")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorCont")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("IdContas");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Contas");
                });

            modelBuilder.Entity("Prospera.Models.Extrato", b =>
                {
                    b.Property<int>("IdExtrato")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdExtrato"));

                    b.Property<DateTime>("DataExtrato")
                        .HasColumnType("datetime2");

                    b.Property<string>("DestinatarioExtrato")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("NomeExtrato")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("ObservacaoExtrato")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int>("RemetenteExtrato")
                        .HasMaxLength(120)
                        .HasColumnType("int");

                    b.Property<string>("StatusExtrato")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal>("ValorExtrato")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("IdExtrato");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Extrato");
                });

            modelBuilder.Entity("Prospera.Models.Terceiros", b =>
                {
                    b.Property<int>("IdTerceiros")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTerceiros"));

                    b.Property<string>("BairroTerceiros")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("CEPTerceiros")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("CidadeTerceiros")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<DateTime>("DataCadastroTerceiros")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataUltimaMovimentacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailTerceiros")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("EnderecoTerceiros")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("NomeTerceiros")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("ObservacaoTerceiros")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("StatusTerceiros")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Telefone2Terceiros")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("TelefoneTerceiros")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UFTerceiros")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("IdTerceiros");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Terceiros");
                });

            modelBuilder.Entity("Prospera.Models.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"));

                    b.Property<string>("CPFUsuario")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CargoUsuario")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<DateTime>("DatCadastroUsuario")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatUltimoAcesUsuario")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailUsuario")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("NomeUsuario")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("SenhaUsuario")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("StatusUsuario")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("IdUsuario");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Prospera.Models.ContaBancaria", b =>
                {
                    b.HasOne("Prospera.Models.Terceiros", "Terceiros")
                        .WithMany()
                        .HasForeignKey("IdTerceiros")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Prospera.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Terceiros");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Prospera.Models.Contas", b =>
                {
                    b.HasOne("Prospera.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Prospera.Models.Extrato", b =>
                {
                    b.HasOne("Prospera.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Prospera.Models.Terceiros", b =>
                {
                    b.HasOne("Prospera.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
