using System.Collections.ObjectModel;

namespace ShoppingCart.Business.Models
{
    public class Endereco : Entity
    {
        public Endereco(string logradouro, string numero, string bairro, string cep, string cidade, string estado, string? complemento = null)
        {
            Logradouro = logradouro;
            Numero = numero;
            Bairro = bairro;
            Cep = cep;
            Cidade = cidade;
            Estado = estado;
            Complemento = complemento;
        }

        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string? Complemento { get; set; }


        public Collection<Cadastro> Cadastros { get; set; }


    }
}
