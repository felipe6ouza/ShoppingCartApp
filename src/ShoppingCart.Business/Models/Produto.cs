using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Models
{
    public class Produto : Entity
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        /* EF Relations*/
        public IEnumerable<ItemPedido> ItemPedidos { get; set; }
    }
}
