using System;
using System.Collections.Generic;

namespace ShoppingCart.Business.Models
{
    public class Pedido : Entity
    {
        public Guid? CadastroId { get; set; }
        public bool Finalizado { get; set; }

        /* EF Relations*/
        public ICollection<ItemPedido> Itens { get; set; }
        public Cadastro Cadastro { get; set; }

        public Pedido()
        {
        }
        public Pedido(Cadastro cadastro)
        {
            Cadastro = cadastro;
            CadastroId = cadastro.Id;
        }

    }
}
