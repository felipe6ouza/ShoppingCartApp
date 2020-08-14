using Microsoft.EntityFrameworkCore;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Business.Models;
using ShoppingCart.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Data.Repository
{
   public class ItemPedidoRepository : Repository<ItemPedido>, IItemPedidoRepository
    {
        public ItemPedidoRepository(ShoppingCartDbContext context): base(context) { }

        public async Task <ICollection<ItemPedido>> ObterItensPedido(Guid pedidoId)
        {
            return await Db.ItemPedidos.Where(i => i.PedidoId == pedidoId).Include(i=> i.Produto).ToListAsync();
        }
    }
}
