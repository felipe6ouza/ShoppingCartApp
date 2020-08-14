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
             .HasColumnType("varchar(100)");

            builder.Property(c => c.Telefone)
             .HasColumnType("varchar(100)");

            builder.Property(c => c.Nome)
             .HasColumnType("varchar(250)");

            builder.Property(c => c.Telefone)
             .HasColumnType("varchar(50)");

            builder.Property(c => c.Logradouro)
             .HasColumnType("varchar(250)");

            builder.Property(c => c.Complemento)
            .HasColumnType("varchar(250)");

            builder.Property(c => c.Cep)
           .HasColumnType("varchar(8)");

            builder.Property(c => c.Numero)
             .HasColumnType("varchar(8)");

            builder.Property(c => c.Cidade)
             .HasColumnType("varchar(50)");

            builder.Property(c => c.Estado)
            .HasColumnType("varchar(50)");

            /*  1 : N Cadastro => Pedido */
            builder.HasMany(p => p.Pedidos).WithOne(p => p.Cadastro)
                .HasForeignKey(p => p.CadastroId);

            builder.ToTable("Cadastros");
        }
    }
}
