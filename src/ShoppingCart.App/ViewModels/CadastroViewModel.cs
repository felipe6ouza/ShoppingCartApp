using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.App.ViewModels
{
    public class CadastroViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo é {0} obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Email { get; set; }

        
        [Required(ErrorMessage = "O campo é {0} obrigatório")]
        [StringLength(250, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo é {0} obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Telefone { get; set; }


        [Required(ErrorMessage = "O campo é {0} obrigatório")]
        [StringLength(250, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O campo é {0} obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Numero { get; set; }

        [Required(ErrorMessage = "O campo é {0} obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "O campo é {0} obrigatório")]
        [StringLength(8, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string Cep { get; set; }

        public string Complemento { get; set; }

        [Required(ErrorMessage = "O campo é {0} obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Cidade { get; set; }


        [Required(ErrorMessage = "O campo é {0} obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Estado { get; set; }

        public IEnumerable<PedidoViewModel> Pedidos { get; set; }

    }
}
