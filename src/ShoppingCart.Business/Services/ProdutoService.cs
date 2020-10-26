using ShoppingCart.Business.Interfaces;
using ShoppingCart.Business.Models;
using ShoppingCart.Business.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Business.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoService(IProdutoRepository produtoRepository, INotificador notificador):base(notificador)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Adicionar(Produto produto)
        {
            // Validar estado da entidade
            // Verificar se existe um produto com mesmo código

            if(!ExecutarValidation(new ProdutoValidation(), produto)) return;

            if(_produtoRepository.Buscar(f => f.Codigo == produto.Codigo).Result.Any())
            {
                Notificar("Já existe um produto com o código informado.");
                return;
            }

            await _produtoRepository.Adicionar(produto);


        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidation(new ProdutoValidation(), produto)) return;

            if (_produtoRepository.Buscar(f => f.Codigo == produto.Codigo).Result.Any())
            {
                Notificar("Já existe um produto com o código informado.");
                return;
            }

            await _produtoRepository.Atualizar(produto);
        }

   
        public async Task Remover(Guid id)
        {
            await _produtoRepository.Remover(id);
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }

    }
}
