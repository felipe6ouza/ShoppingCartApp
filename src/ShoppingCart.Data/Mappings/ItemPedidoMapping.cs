using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCart.Business.Models;

namespace ShoppingCart.Data.Mappings
{
    public class ItemPedidoMapping : IEntityTypeConfiguration<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.PrecoUnitario)
               .IsRequired().HasPrecision(10, 2);

            builder.HasOne(i => i.Produto).WithOne();

            builder.ToTable("ItemPedidos");
        }
    }
}
