using System;
using System.Collections.Generic;

namespace ShoppingCart.Business.Models
{
    public class Cadastro : Entity
    {

        public string Email { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Documento { get; set; }



        /* EF Relations*/
        public ICollection<Pedido> Pedidos { get; set; }
        public ICollection<Endereco> Enderecos { get; set; }

        public Cadastro(string email, string nome, string sobrenome, string telefone, string documento, DateTime dataNascimento)
        {
            Email = email;
            Nome = nome;
            Sobrenome = sobrenome;
            Telefone = telefone;
            Documento = documento;
            DataNascimento = dataNascimento;
        }
    }
}
