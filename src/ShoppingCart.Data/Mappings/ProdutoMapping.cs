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

            builder.Property(p => p.Codigo)
                .IsRequired()
                .HasColumnType("varchar(250)");

            /*  1 : N Produto => ItemPedido (N : 1 ItemPedido => Produto) */

            builder.HasMany(p => p.ItemPedidos).WithOne(p => p.Produto)
                .HasForeignKey(p => p.ProdutoId);

            builder.ToTable("Produtos");
        }
    }
}
