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
        private readonly IItemPedidoRepository _itemPedidoRepository;
        private readonly IPedidoRepository _pedidoRepository;

        public CarrinhoViewComponent(IItemPedidoRepository itemPedidoRepository, IPedidoRepository pedidoRepository)
        {
            _itemPedidoRepository = itemPedidoRepository;
            _pedidoRepository = pedidoRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var pedido = await ObterPedido();
            var Itens = await _itemPedidoRepository.ObterItensPedido(pedido.Id);

            int ItensCarrinho = Itens.Count();
            
            return View(ItensCarrinho);
        }

        private async Task<Pedido> ObterPedido()
        {
            var pedidoId = ObterPedidoId();

            var pedido = await _pedidoRepository.ObterPorId(pedidoId);

            if (pedido != null)
            {
                return pedido;
            }
            else
            {
                var novoPedido = new Pedido();
                await _pedidoRepository.Adicionar(novoPedido);
                await _pedidoRepository.SaveChanges();
                InserirPedidoId(novoPedido.Id);
                return novoPedido;
            }

        }

        private Guid ObterPedidoId()
        {
            byte[] productIdSession = null;

            try
            {
                productIdSession = HttpContext.Session.Get("pedidoId");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (productIdSession != null)
            {
                Guid pedidoIdSession = new Guid(productIdSession);
                return pedidoIdSession;
            }

            return Guid.NewGuid();

        }
        private void InserirPedidoId(Guid pedidoId)
        {
            HttpContext.Session.Set("pedidoId", pedidoId.ToByteArray());
        }

    }
}
