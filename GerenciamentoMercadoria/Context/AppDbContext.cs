using GerenciamentoMercadoria.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoMercadoria.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Mercadoria> Mercadorias { get; set; }
        public DbSet<Fabricante> Fabricantes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Entrada> Entradas { get; set; }
        public DbSet<Saida> Saidas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mercadoria>()
                .HasOne(p => p.categoria)
                .WithMany()
                .HasForeignKey(p => p.CategoriaId);

            modelBuilder.Entity<Mercadoria>()
               .HasOne(p => p.fabricante)
               .WithMany()
               .HasForeignKey(p => p.FabricanteId);

            modelBuilder.Entity<Entrada>()
               .HasOne(p => p.mercadoria)
               .WithMany()
               .HasForeignKey(p => p.MercadoriaId);

            modelBuilder.Entity<Saida>()
               .HasOne(p => p.mercadoria)
               .WithMany()
               .HasForeignKey(p => p.MercadoriaId);
        }
    }
}
