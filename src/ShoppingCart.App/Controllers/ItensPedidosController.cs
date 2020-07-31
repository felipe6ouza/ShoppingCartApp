using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.App.ViewModels;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Business.Models;

namespace ShoppingCart.App.Controllers
{
    public class ItensPedidosController : Controller
    {
        private readonly IItemPedidoRepository _itemPedidoRepository;
        private readonly IMapper _mapper;

        public ItensPedidosController(IItemPedidoRepository itemPedidoRepository, IMapper mapper)
        {
            _itemPedidoRepository = itemPedidoRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ItemPedidoViewModel>>(await _itemPedidoRepository.ObterTodos()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPedidoViewModel = await ObterItemPedidoViewModelPorId(id);
            if (itemPedidoViewModel == null)
            {
                return NotFound();
            }

            return View(itemPedidoViewModel);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemPedidoViewModel itemPedidoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(itemPedidoViewModel);
            }

            var itemPedido = _mapper.Map<ItemPedido>(itemPedidoViewModel);
            await _itemPedidoRepository.Adicionar(itemPedido);
            await _itemPedidoRepository.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPedidoViewModel = await ObterItemPedidoViewModelPorId(id);

            if (itemPedidoViewModel == null)
            {
                return NotFound();
            }
            return View(itemPedidoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ItemPedidoViewModel itemPedidoViewModel)
        {
            if (id != itemPedidoViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var ItemPedido = _mapper.Map<ItemPedido>(itemPedidoViewModel);
                    await _itemPedidoRepository.Adicionar(ItemPedido);
                    await _itemPedidoRepository.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ItemPedidoExists(itemPedidoViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(itemPedidoViewModel);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemPedidoViewModel = await ObterItemPedidoViewModelPorId(id);

            if (itemPedidoViewModel == null)
            {
                return NotFound();
            }

            return View(itemPedidoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _itemPedidoRepository.Remover(id);
            await _itemPedidoRepository.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private async Task<ProdutoViewModel> ObterItemPedidoViewModelPorId(Guid id)
        {
            return Mapper.Map<ProdutoViewModel>(await _itemPedidoRepository.ObterPorId(id));
        }

      
        private async Task<bool> ItemPedidoExists(Guid id)
        {
            var itemPedido = await _itemPedidoRepository.Buscar(e => e.Id == id);

            if (itemPedido == null) return false;

            return true;
        }
    }
}
