using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Models
{
    public class Pedido : Entity
    {
        public Guid CadastroId { get; set; }

        /* EF Relations*/
        public IEnumerable<ItemPedido> Itens { get; set; }
        public Cadastro Cadastro { get; set; }

    }
}
