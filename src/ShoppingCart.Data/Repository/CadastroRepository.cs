using Microsoft.EntityFrameworkCore;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Business.Models;
using ShoppingCart.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Data.Repository
{
    public class CadastroRepository : Repository<Cadastro>, ICadastroRepository
    {
        public CadastroRepository(ShoppingCartDbContext context) : base(context) { }
        
        public async Task<IEnumerable<Pedido>> ObterTodosOsPedidos(Guid CadastroId)
        {
            return await Db.Pedidos.AsNoTracking().Where(p => p.CadastroId == CadastroId).ToListAsync();
        }
    }
}
