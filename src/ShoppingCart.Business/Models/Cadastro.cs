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

        public Cadastro()
        {

        }

        internal void Update(Cadastro novoCadastro)
        {
            Email = novoCadastro.Email;
            Nome = novoCadastro.Nome;
            Telefone = novoCadastro.Telefone;
            Logradouro = novoCadastro.Telefone;
            Numero = novoCadastro.Numero;
            Bairro = novoCadastro.Bairro;
            Cep = novoCadastro.Cep;
            Complemento = novoCadastro.Complemento;
            Cidade = novoCadastro.Cidade;
            Estado = novoCadastro.Estado;

        }

    }
}
