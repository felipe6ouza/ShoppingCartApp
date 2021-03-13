using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.App.ViewModels
{
    public class EnderecoViewModel
    {
        [StringLength(40, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public Collection<CadastroViewModel> Cadastros { get; set; }
    }
}