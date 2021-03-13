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

            builder.Property(p => p.Finalizado)
            .HasDefaultValue(false);

            builder.HasOne(c => c.Cadastro)
                .WithMany(p => p.Pedidos)
                .HasForeignKey(c => c.CadastroId);


            builder.ToTable("Pedidos");
        }
    } 
}
