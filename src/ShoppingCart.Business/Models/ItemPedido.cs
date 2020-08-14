using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public ItemPedido(Pedido pedido, Produto produto, int quantidade)
        {
            ProdutoId = produto.Id;
            PedidoId = pedido.Id;           
            Quantidade = quantidade;
            PrecoUnitario = produto.Preco;


        }

        public ItemPedido()
        {

        }
    }
}
