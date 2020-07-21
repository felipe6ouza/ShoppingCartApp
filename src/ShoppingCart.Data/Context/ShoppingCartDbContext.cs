using Microsoft.EntityFrameworkCore;
using ShoppingCart.Business.Models;

namespace ShoppingCart.Data.Context
{
    public class ShoppingCartDbContext : DbContext
    {
        public ShoppingCartDbContext(DbContextOptions <ShoppingCartDbContext> options) : base(options) { }
        public DbSet<Cadastro> Cadastros { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItemPedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
              
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShoppingCartDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

    }
}
