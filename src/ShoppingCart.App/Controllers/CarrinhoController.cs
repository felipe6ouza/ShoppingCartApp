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
            IItemPedidoRepository itemPedidoRepository, IMapper mapper, IPedidoRepository pedidoRepository)
        {
            _produtoRespository = produtoRespository;
            _itemPedidoRepository = itemPedidoRepository;
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var pedido = await ObterPedido();
            var itensPedido = await _itemPedidoRepository.ObterItensPedido(pedido.Id);

            var itens = _mapper.Map<List<ItemPedidoViewModel>>(itensPedido);
            var carrinho = new CarrinhoViewModel(itens);

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
        public async Task<IActionResult> AtualizarItemCarrinho([FromBody]object response)
        {
            var responseString = response.ToString();
            AlterarQuantidade alterarQuantidade = JsonConvert.DeserializeObject<AlterarQuantidade>(responseString);

            var pedido = await ObterPedido();
            var itensPedido = await _itemPedidoRepository.ObterItensPedido(pedido.Id);

            foreach(var item in itensPedido)
            {
                if(item.Produto.Codigo == alterarQuantidade.Codigo)
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
