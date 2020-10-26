using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.App.ViewModels
{
    public class CarrinhoViewModel
    {
        public List<ItemPedidoViewModel> Itens = null;

        public decimal ItensQuantidade
        {
            get
            {
                return Itens != null ? Itens.Count  : 0;
            }
        }

        public decimal Total
        {
            get
            {
                return Itens.Sum(i => i.PrecoUnitario * i.Quantidade);
            }
        }

        public CarrinhoViewModel(List<ItemPedidoViewModel> itens)
        {
            Itens = itens;
        }
        
    }
}
