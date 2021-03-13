using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCart.Business.Models;

namespace ShoppingCart.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(p => p.Preco)
                .IsRequired().HasPrecision(10, 2);
                

            builder.Property(p => p.Descricao)
               .IsRequired()
               .HasColumnType("varchar(4000)");

            builder.Property(p => p.Imagem)
                .IsRequired()
                .HasColumnType("varchar(max)");

            builder.Property(p => p.Codigo)
                .ValueGeneratedOnAdd();

            builder.ToTable("Produtos");
        }
    }
}
