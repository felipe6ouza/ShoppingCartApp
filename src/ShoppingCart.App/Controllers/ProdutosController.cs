using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.App.ViewModels;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Business.Models;

namespace ShoppingCart.App.Controllers
{
    public class ProdutosController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        public ProdutosController(IProdutoRepository produtoRepository, IMapper mapper, IProdutoService produtoService,
            INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos()));
        }

        public IActionResult Sobre()
        {
            return View();
        }


        public async Task<IActionResult> Catalogo()
        {
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoViewModel = await ObterProdutoViewModelPorId(id);

            if (produtoViewModel == null)
            {
                return NotFound();
            }

            return View(produtoViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return View(produtoViewModel);

            var imgPrefixo = Guid.NewGuid() + "_";

            if(! await UploadArquivo(produtoViewModel.ImagemUpload, imgPrefixo))
            {
                return View(produtoViewModel);
            }

            produtoViewModel.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;
            
            var produto = _mapper.Map<Produto>(produtoViewModel);
            await _produtoService.Adicionar(produto);

            if (!OperacaoValida()) return View(produtoViewModel);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoViewModel = await ObterProdutoViewModelPorId(id);
            if (produtoViewModel == null)
            {
                return NotFound();
            }
            return View(produtoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(produtoViewModel);

            var produto = _mapper.Map<Produto>(produtoViewModel);
            await _produtoService.Atualizar(produto);

              if (!OperacaoValida()) return View(produtoViewModel);

          

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null) return NotFound();
           
            var produtoViewModel = await ObterProdutoViewModelPorId(id);
            
            if (produtoViewModel == null) return NotFound();
            
            return View(produtoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produtoViewModel = await ObterProdutoViewModelPorId(id);

            if(produtoViewModel == null)
            {
                return NotFound();
            }

            await _produtoService.Remover(id);
            if (!OperacaoValida()) return View(produtoViewModel);


            return RedirectToAction(nameof(Index));
        }

        private async Task<ProdutoViewModel> ObterProdutoViewModelPorId(Guid id)
        {
            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPorId(id));
        }

        private async Task<bool> ProdutoExists(Guid id)
        {
            var produto = await _produtoRepository.Buscar(e => e.Id == id);

            if (produto == null) return false;

            return true;
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string prefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imgUploads", prefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com esse nome!");
                return false;
            }

            using(var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }
    }
}
