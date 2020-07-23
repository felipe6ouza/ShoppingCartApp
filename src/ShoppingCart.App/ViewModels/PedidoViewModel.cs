using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.App.ViewModels
{
    public class PedidoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public IEnumerable<ItemPedidoViewModel> Itens { get; set; }
        public CadastroViewModel Cadastro { get; set; }
    }
}
