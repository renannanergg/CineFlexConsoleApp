using CineFlexConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CineFlexConsoleApp.Data.Mappings
{
    public class FilmeMap : IEntityTypeConfiguration<Filme>
    {
        public void Configure(EntityTypeBuilder<Filme> builder)
        {
            builder.ToTable("Filme"); // Nome da tabela no banco

            // Chave primária
            builder.HasKey(x => x.Id);  

            // Configura a propriedade Id como identity (auto incremento)
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(); 
            
            // Propriedades
            builder.Property(x => x.Titulo)
                .IsRequired()
                .HasColumnName("Titulo")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);
            
            builder.Property(x => x.Duracao)
                .IsRequired()
                .HasColumnName("Duracao");
            
            builder.Property(x => x.Genero)
                .IsRequired()
                .HasColumnName("Genero")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);
            
            // Relacionamentos
            builder
                .HasMany(x => x.Sessoes)  // um filme tem muitas sessões
                .WithOne(x => x.Filme)    // cada sessão tem um único filme.
                .HasForeignKey(x => x.FilmeId) // define qual propriedade em Sessao representa a chave estrangeira.
                .OnDelete(DeleteBehavior.Cascade); // Se um filme for deletado, suas sessões também

            

        }
    }
}