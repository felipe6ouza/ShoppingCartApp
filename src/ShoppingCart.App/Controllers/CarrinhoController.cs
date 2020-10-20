using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            IItemPedidoRepository itemPedidoRepository, IMapper mapper, IPedidoRepository pedidoRepository, INotificador notificador):base(notificador)
        {
            _produtoRespository = produtoRespository;
            _itemPedidoRepository = itemPedidoRepository;
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var pedido = await ObterPedido();

            var itensPedido = _mapper.Map<List<ItemPedidoViewModel>>(pedido.Itens);

            var carrinho = new CarrinhoViewModel(itensPedido);

            return View(carrinho);
        }

        public async Task<IActionResult> AdicionarItem(string Codigo)
        {
            var produto = await _produtoRespository.ObterPorCodigo(Codigo);

            if (produto == null)
                return NotFound();

            var pedido = await ObterPedido();
            var itensPedido = await _itemPedidoRepository.ObterItensPedido(pedido.Id);


            var produtoExistente = (from item in itensPedido
                                    where item.Produto.Id.Equals(produto.Id)
                                    select item).FirstOrDefault();

            if (produtoExistente == null)
            {
                var novoItem = new ItemPedido(pedido, produto, 1);
                await _itemPedidoRepository.Adicionar(novoItem);
                await _itemPedidoRepository.SaveChanges();
                novoItem.Produto = produto;
                itensPedido.Add(novoItem);
            }

            else
            {
                produtoExistente.Quantidade++;
                await _itemPedidoRepository.Atualizar(produtoExistente);
                await _itemPedidoRepository.SaveChanges();
            }

            return RedirectToAction(nameof(Index));


        }
        [HttpPost]
        public async Task<IActionResult> RemoverItem(Guid itemPedidoId)
        {
            var item = itemPedidoId;
            await _itemPedidoRepository.Remover(itemPedidoId);
            await _itemPedidoRepository.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> AtualizarItemCarrinho([FromBody] object response)
        {
            var responseString = response.ToString();
            AlterarQuantidade alterarQuantidade = JsonConvert.DeserializeObject<AlterarQuantidade>(responseString);

            if (alterarQuantidade.Quantidade < 1)
            {
                return RedirectToAction(nameof(Index));
            }

            var pedido = await ObterPedido();
            var itensPedido = await _itemPedidoRepository.ObterItensPedido(pedido.Id);



            foreach (var item in itensPedido)
            {
                if (item.Produto.Nome == alterarQuantidade.Nome)
                {
                    item.Quantidade = alterarQuantidade.Quantidade;
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
