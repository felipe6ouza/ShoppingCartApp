using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.App.ViewComponents
{
    [ViewComponent(Name = "Carrinho")]
    public class CarrinhoViewComponent : ViewComponent
    {
        private readonly IPedidoRepository _pedidoRepository;

        public CarrinhoViewComponent(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var pedidoId = ObterPedidoId();

            int ItensCarrinho = 0;

            if (pedidoId != Guid.Empty)
            {
                var pedido = await _pedidoRepository.ObterDadosPedido(pedidoId);
                ItensCarrinho = pedido.Itens.Count();
            }

            return View(ItensCarrinho);
        }

        private Guid ObterPedidoId()
        {

            var pedidoIdSession = HttpContext.Session.GetString("pedidoId");

            if (string.IsNullOrEmpty(pedidoIdSession))
            {
                return Guid.Empty;
            }
            return Guid.Parse(pedidoIdSession);

        }
      
    }
}
