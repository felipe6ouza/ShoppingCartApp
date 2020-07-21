using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Models
{
    public class ItemPedido : Entity
    {
        public Guid PedidoId { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }


        /* EF Relations*/
        public Pedido Pedido { get; set; }
        public Produto Produto { get; set; }


    }
}
