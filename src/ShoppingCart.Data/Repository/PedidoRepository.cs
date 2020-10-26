using Microsoft.EntityFrameworkCore;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Business.Models;
using ShoppingCart.Data.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Data.Repository
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(ShoppingCartDbContext context) : base(context) { }

        public async Task<Pedido> ObterDadosPedido(Guid Id)
        {
            return await Db.Pedidos.AsNoTracking()
                 .Include(p => p.Itens).ThenInclude(i => i.Produto)
                 .Include(p => p.Cadastro)
                 .Where(p => p.Id == Id)
                 .SingleOrDefaultAsync();
        }
    }
}
