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
             .HasColumnType("varchar(100)");

            builder.Property(c => c.Telefone)
             .IsRequired()
             .HasColumnType("varchar(100)");

            builder.Property(c => c.Nome)
             .IsRequired()
             .HasColumnType("varchar(250)");

            builder.Property(c => c.Telefone)
             .IsRequired()
             .HasColumnType("varchar(50)");

            builder.Property(c => c.Logradouro)
             .IsRequired()
             .HasColumnType("varchar(250)");

            builder.Property(c => c.Complemento)
            .IsRequired()
            .HasColumnType("varchar(250)");

            builder.Property(c => c.Cep)
           .IsRequired()
           .HasColumnType("varchar(8)");

            builder.Property(c => c.Numero)
             .IsRequired()
             .HasColumnType("varchar(8)");

            builder.Property(c => c.Cidade)
             .IsRequired()
             .HasColumnType("varchar(50)");

            builder.Property(c => c.Estado)
            .IsRequired()
            .HasColumnType("varchar(50)");

            /*  1 : N Cadastro => Pedido */
            builder.HasMany(p => p.Pedidos).WithOne(p => p.Cadastro)
                .HasForeignKey(p => p.CadastroId);

            builder.ToTable("Cadastros");
        }
    }
}
