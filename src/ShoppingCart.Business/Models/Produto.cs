using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Models
{
    public class Produto : Entity
    {
        public Produto(string nome, string descricao, string imagem, decimal preco)
        {
            Nome = nome;
            Descricao = descricao;
            Imagem = imagem;
            Preco = preco;
        }

        public int Codigo { get; private set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal Preco { get; set; }

       
    }
}
