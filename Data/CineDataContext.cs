using CineFlexConsoleApp.Data.Mappings;
using CineFlexConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CineFlexConsoleApp.Data
{
    public class CineDataContext : DbContext
    {
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Ingresso> Ingressos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Connection_String");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FilmeMap());
            modelBuilder.ApplyConfiguration(new IngressoMap());
            modelBuilder.ApplyConfiguration(new SessaoMap());
            modelBuilder.ApplyConfiguration(new SalaMap());
        }

    }
}