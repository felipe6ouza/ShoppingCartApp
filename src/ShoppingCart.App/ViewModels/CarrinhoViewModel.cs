using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.App.ViewModels
{
    public class CarrinhoViewModel
    {
        public IList<ItemPedidoViewModel> Itens { get; private set; }
        public decimal Total
        {
            get
            {
                return Itens.Sum(i => i.PrecoUnitario * i.Quantidade);
            }
        }

        public CarrinhoViewModel(IList<ItemPedidoViewModel> itens)
        {
            Itens = itens;
        }
    }
}
