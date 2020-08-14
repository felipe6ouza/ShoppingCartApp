using Microsoft.EntityFrameworkCore;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Business.Models;
using ShoppingCart.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ShoppingCartDbContext context): base(context) { }

        public async Task<Produto> ObterPorCodigo(string Codigo)
        {
            return await Db.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Codigo == Codigo);
        }
    }
}
