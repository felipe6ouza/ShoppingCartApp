using System;
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
    public class CadastrosController : Controller
    {
        private readonly ICadastroRepository _cadastroRepository;
        private readonly IMapper _mapper;
        public CadastrosController(ICadastroRepository cadastroRepository, IMapper mapper)
        {
            _cadastroRepository = cadastroRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<CadastroViewModel>(await _cadastroRepository.ObterTodos()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadastroViewModel = await ObterCadastroViewModelPorId(id);
            if (cadastroViewModel == null)
            {
                return NotFound();
            }

            return View(cadastroViewModel);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CadastroViewModel cadastroViewModel)
        {
            if (!ModelState.IsValid)
                return View(cadastroViewModel);

            var cadastro = _mapper.Map<Cadastro>(cadastroViewModel);

            await _cadastroRepository.Adicionar(cadastro);
            await _cadastroRepository.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadastroViewModel = await ObterCadastroViewModelPorId(id);
            
            if (cadastroViewModel == null)
            {
                return NotFound();
            }
            return View(cadastroViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CadastroViewModel cadastroViewModel)
        {
            if (id != cadastroViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var cadastro = _mapper.Map<Cadastro>(cadastroViewModel);
                    await _cadastroRepository.Atualizar(cadastro);
                    await _cadastroRepository.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CadastroExists(cadastroViewModel.Id))
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
            return View(cadastroViewModel);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cadastroViewModel = await ObterCadastroViewModelPorId(id);
            
            if (cadastroViewModel == null)
            {
                return NotFound();
            }

            return View(cadastroViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
           await _cadastroRepository.Remover(id);
           await _cadastroRepository.SaveChanges();

           return RedirectToAction(nameof(Index));

        }

        private async Task<CadastroViewModel> ObterCadastroViewModelPorId(Guid id)
        {
            return _mapper.Map<CadastroViewModel>(await _cadastroRepository.ObterPorId(id));
        }

        private async Task<bool> CadastroExists(Guid id)
        {
            var cadastro = await _cadastroRepository.Buscar(e => e.Id == id);

            if (cadastro == null) return false;

            return true;
        }
    }
}
