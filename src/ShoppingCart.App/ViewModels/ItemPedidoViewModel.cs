using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.App.ViewModels
{
    public class ItemPedidoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }


        public PedidoViewModel Pedido { get; set; }
        public PedidoViewModel Produto { get; set; }
    }
}
