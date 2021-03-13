using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.App.Areas.Identity.Data;
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
        private readonly IItemPedidoRepository _itemPedidoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;
  

        public CarrinhoController(IPedidoRepository pedidoRepository, IItemPedidoRepository itemPedidoRepository,
            IProdutoRepository produtoRepository, UserManager<Usuario> userManager,
            IMapper mapper, IHttpContextAccessor accessor, INotificador notificador):base(notificador, mapper)
        {
            _itemPedidoRepository = itemPedidoRepository;
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var pedido = await ObterPedido();

            var itensPedido = _mapper.Map<List<ItemPedidoViewModel>>(pedido.Itens);

            var carrinho = new CarrinhoViewModel(itensPedido);

            return View(carrinho);
        }

        public async Task<IActionResult> AdicionarItem(int Codigo)
        {
            var produto = await _produtoRepository.ObterPorCodigo(Codigo);

            if (produto == null)
                return NotFound();

            var pedido = await ObterPedido();


            if (pedido.Itens == null)
            {
                var novoItemPedido = new ItemPedido(pedido, produto, 1);
                await _itemPedidoRepository.Adicionar(novoItemPedido);
                novoItemPedido.Produto = produto;
                pedido.Itens.Add(novoItemPedido);
                
                return RedirectToAction(nameof(Index));
            }

            var ItemNoCarrinho = pedido.Itens.Where(c => c.ProdutoId.Equals(produto.Id)).FirstOrDefault();


            if (ItemNoCarrinho != null)
            {
                ItemNoCarrinho.Quantidade++;
                await _itemPedidoRepository.Atualizar(ItemNoCarrinho);
            }

            else
            {
                var novoItemCarrinho = new ItemPedido(pedido, produto, 1);
                await _itemPedidoRepository.Adicionar(novoItemCarrinho);
                pedido.Itens.Add(novoItemCarrinho);
            }

            return RedirectToAction(nameof(Index));


        }
        
        [HttpPost]
        public async Task<IActionResult> RemoverItem(Guid itemPedidoId)
        {
            await _itemPedidoRepository.Remover(itemPedidoId);
            await _itemPedidoRepository.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> AtualizarQuantidade([FromBody] AlterarQuantidadeItemPedido dadosAlteracao)
        { 

            if (dadosAlteracao.Quantidade < 1)
            {
                return RedirectToAction(nameof(Index));
            }

            var pedido = await ObterPedido();

            foreach (var item in pedido.Itens)
            {
                if (item.Produto.Nome.Equals(dadosAlteracao.Nome))
                {
                    item.Quantidade = dadosAlteracao.Quantidade;
                    await _itemPedidoRepository.Atualizar(item);
                    break;
                }
            }


            return RedirectToAction(nameof(Index));
        }

        private async Task<Pedido> ObterPedido()
        {
            var pedidoId = ObterPedidoId();

            if(pedidoId == Guid.Empty)
            {
                var novoPedido = new Pedido();
                await _pedidoRepository.Adicionar(novoPedido);
                InserirSessionPedidoId(novoPedido.Id);
                return novoPedido;
            }

            var pedido = await _pedidoRepository.ObterDadosPedido(pedidoId);
            return pedido;

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
        private void InserirSessionPedidoId(Guid pedidoId)
        {
            HttpContext.Session.SetString("pedidoId", pedidoId.ToString());
        }


    }
}
