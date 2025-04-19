using CineFlexConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CineFlexConsoleApp.Data.Mappings
{
    public class SessaoMap : IEntityTypeConfiguration<Sessao>
    {
        public void Configure(EntityTypeBuilder<Sessao> builder)
        {
            builder.ToTable("Sessao"); // Nome da tabela no banco

            // Chave primária
            builder.HasKey(x => x.Id);

            // Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Horário da sessão (ex: 2025-04-11 19:30:00)
            builder.Property(x => x.Horario)
                .IsRequired()
                .HasColumnName("Horario")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()");

            // Foreign Key: Filme
            builder.Property(x => x.FilmeId)
                .IsRequired()
                .HasColumnName("FilmeId");

            builder
                .HasOne(x => x.Filme)
                .WithMany(x => x.Sessoes)
                .HasForeignKey(x => x.FilmeId)
                .OnDelete(DeleteBehavior.Cascade); // Ao deletar o filme, apaga as sessões

            // Foreign Key: Sala
            builder.Property(x => x.SalaId)
                .IsRequired()
                .HasColumnName("SalaId");

            builder
                .HasOne(x => x.Sala)
                .WithMany(x => x.Sessoes)
                .HasForeignKey(x => x.SalaId)
                .OnDelete(DeleteBehavior.Cascade); // Ao deletar a sala, apaga as sessões

            // Relacionamento com Ingressos: uma sessão tem muitos ingressos
            builder
                .HasMany(x => x.Ingressos)
                .WithOne(x => x.Sessao)
                .HasForeignKey(x => x.SessaoId)
                .OnDelete(DeleteBehavior.Cascade); // Ao deletar a sessão, apaga todos os ingressos
        }
    }
}