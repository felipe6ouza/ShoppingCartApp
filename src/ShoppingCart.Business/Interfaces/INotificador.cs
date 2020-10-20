using ShoppingCart.Business.Notificacoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Business.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();

        void Handle(Notificacao notificacao);
    }
}
