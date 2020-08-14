using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.App.ViewModels
{
    public class PedidoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public List<ItemPedidoViewModel> Itens = new List<ItemPedidoViewModel>();
        public CadastroViewModel Cadastro { get; set; }
    }
}
