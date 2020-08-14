using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Models
{
    public class Pedido : Entity
    {
        public Guid CadastroId { get; set; }

        /* EF Relations*/
        public ICollection<ItemPedido> Itens { get; set; }
        public Cadastro Cadastro { get; set; }

        public Pedido()
        {
            Cadastro = new Cadastro();
            CadastroId = Cadastro.Id;
        }
        public Pedido(Cadastro cadastro)
        {
            Cadastro = cadastro;
            CadastroId = cadastro.Id;
        }

    }
}
