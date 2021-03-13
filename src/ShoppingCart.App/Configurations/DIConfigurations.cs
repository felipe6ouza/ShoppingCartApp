using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.App.Data;
using ShoppingCart.Business.Interfaces;
using ShoppingCart.Business.Notificacoes;
using ShoppingCart.Business.Services;
using ShoppingCart.Data.Context;
using ShoppingCart.Data.Repository;

namespace ShoppingCart.App.Configurations
{
    public static class DIConfigurations
    {
        public static IServiceCollection ResolveDependencies (this IServiceCollection services)
        {
            services.AddScoped<ShoppingCartDbContext>();
            services.AddScoped<IdentityContext>();
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
