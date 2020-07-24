using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.App.ViewModels
{
    public class ProdutoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo é {0} obrigatório")]
        [StringLength(250, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "O campo é {0} obrigatório")]
        [StringLength(250, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }
       
        [Required(ErrorMessage = "O campo é {0} obrigatório")]
        [StringLength(500, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }
        public string Imagem { get; set; }

        [DisplayName("Preço")]
        [Required(ErrorMessage = "O campo é {0} obrigatório")]
        public decimal Preco { get; set; }

        public IEnumerable<ItemPedidoViewModel> ItemPedidos { get; set; }
    }
}
