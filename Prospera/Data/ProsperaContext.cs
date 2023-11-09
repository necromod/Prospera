using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prospera.Models;

namespace Prospera.Data
{
    public class ProsperaContext : DbContext
    {
        public ProsperaContext (DbContextOptions<ProsperaContext> options)
            : base(options)
        {
        }

        public DbSet<Prospera.Models.ContaBancaria> ContaBancaria { get; set; } = default!;

        public DbSet<Prospera.Models.Contas>? Contas { get; set; }

        public DbSet<Prospera.Models.Usuario>? Usuario { get; set; }

        public DbSet<Prospera.Models.Terceiros>? Terceiros { get; set; }

        public DbSet<Prospera.Models.Extrato>? Extrato { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapear o campo 'SaldoContBan' na tabela 'ContaBancaria'.
            modelBuilder.Entity<ContaBancaria>()
                .Property(c => c.SaldoContBan)
                .HasColumnType("decimal(18, 2)"); // Exemplo: 18 dígitos no total, 2 casas decimais.

            // Mapear o campo 'ValorCont' na tabela 'Contas'.
            modelBuilder.Entity<Contas>()
                .Property(c => c.ValorCont)
                .HasColumnType("decimal(18, 2)");

            // Mapear o campo 'ValorExtrato' na tabela 'Extrato'.
            modelBuilder.Entity<Extrato>()
                .Property(e => e.ValorExtrato)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Contas>()
       .Property(c => c.ValorCont)
       .HasColumnType("decimal(18, 2)");


            modelBuilder.Entity<ContaBancaria>()
    .HasOne(cb => cb.Terceiros)
    .WithMany()
    .HasForeignKey(cb => cb.IdTerceiros)
    .OnDelete(DeleteBehavior.NoAction);





        }
    }
}
