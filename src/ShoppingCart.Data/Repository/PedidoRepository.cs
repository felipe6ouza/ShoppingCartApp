using ShoppingCart.Business.Interfaces;
using ShoppingCart.Business.Models;
using ShoppingCart.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Data.Repository
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(ShoppingCartDbContext context) : base(context) { }
        
    }
}
