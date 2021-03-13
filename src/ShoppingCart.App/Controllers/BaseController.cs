using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Business.Interfaces;

namespace ShoppingCart.App.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly INotificador _notificador;
        internal readonly IMapper _mapper;

        public BaseController(INotificador notificador, IMapper mapper)
        {
            _notificador = notificador;
            _mapper = mapper;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }
    }
}
