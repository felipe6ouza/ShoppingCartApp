using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.App.ViewModels;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.App.Controllers
{
    public class CarrinhoController : BaseController
    {
        private readonly IProdutoRepository _produtoRespository;
        private readonly IItemPedidoRepository _itemPedidoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public CarrinhoController(IProdutoRepository produtoRespository,
            IItemPedidoRepository itemPedidoRepository, IMapper mapper, IPedidoRepository pedidoRepository)
        {
            _produtoRespository = produtoRespository;
            _itemPedidoRepository = itemPedidoRepository;
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Carrinho(string Codigo)
        {
            var produto = await _produtoRespository.ObterPorCodigo(Codigo);

            if (produto == null)
                return NotFound();

            var pedido = await ObterPedido();
          
            var ItensPedido = await _itemPedidoRepository.ObterItensPedido(pedido.Id);
            

            foreach(var item in ItensPedido)
            {
                if (item.ProdutoId == produto.Id)
                {
                    item.Quantidade++;
                    await _itemPedidoRepository.Atualizar(item);
                }
            }

            int x = (from i in ItensPedido
                     where i.ProdutoId == produto.Id
                     select i).Count();

            if (x == 0)
            {
                var novoItem = new ItemPedido(pedido, produto, 1);
                await _itemPedidoRepository.Adicionar(novoItem);
                await _itemPedidoRepository.SaveChanges();
                novoItem.Produto = produto;
                ItensPedido.Add(novoItem);
            }
           

            var itens = _mapper.Map<List<ItemPedidoViewModel>>(ItensPedido);
            var carrinhoViewModel = new CarrinhoViewModel(itens);
            return View("Index", carrinhoViewModel);

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
