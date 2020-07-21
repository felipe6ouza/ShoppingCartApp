using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCart.Business.Models;

namespace ShoppingCart.Data.Mappings
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(p => p.Id);

            /*  1 : N Pedido => ItemPedido */

            builder.HasMany(p => p.Itens).WithOne(p => p.Pedido)
                .HasForeignKey(p => p.PedidoId);

            builder.ToTable("Pedidos");
        }
    } 
}
