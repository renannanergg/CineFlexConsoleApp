using CineFlexConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CineFlexConsoleApp.Data.Mappings
{
    public class SalaMap : IEntityTypeConfiguration<Sala>
    {
        public void Configure(EntityTypeBuilder<Sala> builder)
        {
            builder.ToTable("Sala"); // Nome da tabela no banco

            // Chave primária
            builder.HasKey(x => x.Id); 

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(); 

            // Número da sala (pode ser usado para identificar: Sala 1, Sala 2, etc.)
            builder.Property(x => x.NumeroDaSala)
                .IsRequired()
                .HasColumnName("NumeroDaSala")
                .HasColumnType("INT");
            
            // Capacidade de assentos da sala
            builder.Property(x => x.Capacidade)
                .IsRequired()
                .HasColumnName("Capacidade")
                .HasColumnType("INT");

            // Relacionamento: 1 Sala → muitas Sessoes
            builder
                .HasMany(x => x.Sessoes)
                .WithOne(x => x.Sala)
                .HasForeignKey(x => x.SalaId)
                .OnDelete(DeleteBehavior.Cascade); // Ao deletar a sala, remove também as sessões
        }
    }
}