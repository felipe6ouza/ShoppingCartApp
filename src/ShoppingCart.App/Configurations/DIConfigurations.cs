using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Business.Notificacoes;
using ShoppingCart.Business.Services;
using ShoppingCart.Data.Context;
using ShoppingCart.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.App.Configurations
{
    public static class DIConfigurations
    {
        public static IServiceCollection ResolveDependencies (this IServiceCollection services)
        {
            services.AddScoped<ShoppingCartDbContext>();
            services.AddScoped<ICadastroRepository, CadastroRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IItemPedidoRepository, ItemPedidoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IProdutoService, ProdutoService>();

            return services;
        }
            

    }
}
