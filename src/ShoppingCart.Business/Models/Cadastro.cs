using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Models
{
    public class Cadastro : Entity
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        /* EF Relations*/
        public IEnumerable<Pedido> Pedidos { get; set; }

    }
}
