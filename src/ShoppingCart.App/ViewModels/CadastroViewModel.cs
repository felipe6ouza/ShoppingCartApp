using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.App.ViewModels
{
    public class CadastroViewModel
    {
        [Required(ErrorMessage = "O campo é {0} obrigatório")]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo é {0} obrigatório")]
        [StringLength(40, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo é {0} obrigatório")]
        [StringLength(80, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Sobrenome { get; set; }
       
        [DisplayName("Data Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo é {0} obrigatório")]
        [Phone]
        [StringLength(20, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo é {0} obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Documento { get; set; }


        /* EF Relations*/
        public ICollection<PedidoViewModel> Pedidos { get; set; }
        public ICollection<EnderecoViewModel> Enderecos { get; set; }

    }
}