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
    public class PedidosController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;


        public PedidosController(IPedidoRepository pedidoRepository, IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<PedidoViewModel>>(await _pedidoRepository.ObterTodos()));

        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoViewModel = await ObterPedidoViewModelPorId(id);

            if (pedidoViewModel == null)
            {
                return NotFound();
            }

            return View(pedidoViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PedidoViewModel pedidoViewModel)
        {
            if (!ModelState.IsValid)
                return View(pedidoViewModel);

            var pedido = _mapper.Map<Pedido>(pedidoViewModel);

            await _pedidoRepository.Adicionar(pedido);
            await _pedidoRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoViewModel = await ObterPedidoViewModelPorId(id);

            if (pedidoViewModel == null)
            {
                return NotFound();
            }
            return View(pedidoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PedidoViewModel pedidoViewModel)
        {
            if (id != pedidoViewModel.Id)
                return NotFound();
            
            if (!ModelState.IsValid)
                return View(pedidoViewModel);
           
            try
            {
                var pedido = _mapper.Map<Pedido>(pedidoViewModel);
                await _pedidoRepository.Atualizar(pedido);
                await _pedidoRepository.SaveChanges();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!await PedidoExists(pedidoViewModel.Id))
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

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoViewModel = await ObterPedidoViewModelPorId(id);

            if (pedidoViewModel == null)
            {
                return NotFound();
            }

            return View(pedidoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _pedidoRepository.Remover(id);
            await _pedidoRepository.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private async Task<PedidoViewModel> ObterPedidoViewModelPorId(Guid id)
        {
            return _mapper.Map<PedidoViewModel>(await _pedidoRepository.ObterPorId(id));
        }

        private async Task<bool> PedidoExists(Guid id)
        {
            var pedido = await _pedidoRepository.Buscar(e => e.Id == id);

            if (pedido == null) return false;

            return true;
        }
    }
}
