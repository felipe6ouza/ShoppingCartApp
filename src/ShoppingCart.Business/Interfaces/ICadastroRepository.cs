using ShoppingCart.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Business.Interfaces
{
    public interface ICadastroRepository : IRepository<Cadastro>
    {
        Task<IEnumerable<Pedido>> ObterTodosOsPedidos(Guid id);
    }
}
