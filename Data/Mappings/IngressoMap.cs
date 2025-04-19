using CineFlexConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CineFlexConsoleApp.Data.Mappings
{
    public class IngressoMap : IEntityTypeConfiguration<Ingresso>
    {
        public void Configure(EntityTypeBuilder<Ingresso> builder)
        {
            builder.ToTable("Ingresso"); // Nome da tabela no banco

            // Chave primária
            builder.HasKey(x => x.Id);

            // Configura o Id como identity (auto incremento)
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            // Assento do ingresso (ex: "B12", "A1", etc.)
            builder.Property(x => x.Assento)
                .IsRequired()
                .HasColumnName("Assento")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(10);

            // Preço do ingresso
            builder.Property(x => x.Preco)
                .IsRequired()
                .HasColumnName("Preco")
                .HasColumnType("DECIMAL(10,2)");

            // Chave estrangeira: SessaoId
            builder.Property(x => x.SessaoId)
                .IsRequired()
                .HasColumnName("SessaoId");

            // Relacionamento: muitos ingressos → 1 sessão
            builder
                .HasOne(x => x.Sessao)
                .WithMany(x => x.Ingressos)
                .HasForeignKey(x => x.SessaoId)
                .OnDelete(DeleteBehavior.Cascade); // Ao deletar uma sessão, apaga também os ingressos
        }
    }
}