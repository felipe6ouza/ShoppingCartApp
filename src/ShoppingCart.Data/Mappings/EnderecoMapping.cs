using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCart.Business.Models;

namespace ShoppingCart.Data.Mappings
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Logradouro)
                .IsRequired()
             .HasColumnType("varchar(50)");

            builder.Property(e => e.Bairro)
                  .IsRequired()
             .HasColumnType("varchar(35)");

            builder.Property(e => e.Cidade)
                  .IsRequired()
             .HasColumnType("varchar(35)");

            builder.Property(e => e.Estado)
                  .IsRequired()
             .HasColumnType("varchar(35)");

            builder.Property(e => e.Numero)
                  .IsRequired()
             .HasColumnType("varchar(7)");

            builder.Property(e => e.Cep)
                  .IsRequired()
             .HasColumnType("varchar(9)");

            builder.Property(e => e.Complemento)
              .HasColumnType("varchar(70)");

            builder.ToTable("Enderecos");
        }
    }
}
