using ShoppingCart.Business.Interfaces;
using ShoppingCart.Business.Models;
using ShoppingCart.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Data.Repository
{
   public class ItemPedidoRepository : Repository<ItemPedido>, IItemPedidoRepository
    {
        public ItemPedidoRepository(ShoppingCartDbContext context): base(context) { }
       
    }
}
