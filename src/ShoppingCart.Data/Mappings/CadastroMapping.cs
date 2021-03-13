using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCart.Business.Models;

namespace ShoppingCart.Data.Mappings
{
    public class CadastroMapping : IEntityTypeConfiguration<Cadastro>
    {
        public void Configure(EntityTypeBuilder<Cadastro> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Email)
                  .IsRequired()
             .HasColumnType("varchar(50)");

            builder.Property(c => c.Telefone)
                  .IsRequired()
             .HasColumnType("varchar(20)");

            builder.Property(c => c.Nome)
                  .IsRequired()
             .HasColumnType("varchar(50)");

            builder.Property(c => c.Sobrenome)
                  .IsRequired()
            .HasColumnType("varchar(80)");

            builder.Property(c => c.Documento)
                  .IsRequired()
            .HasColumnType("varchar(14)");

            builder.Property(c => c.DataNascimento)
                  .IsRequired()
            .HasColumnType("datetime2");


            builder.ToTable("Cadastros");
        }
    }
}
