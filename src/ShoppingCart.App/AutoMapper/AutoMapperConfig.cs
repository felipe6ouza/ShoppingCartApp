using AutoMapper;
using ShoppingCart.App.ViewModels;
using ShoppingCart.Business.Models;

namespace ShoppingCart.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Cadastro, CadastroViewModel>().ReverseMap();
            CreateMap<Pedido, PedidoViewModel>().ReverseMap();
            CreateMap<ItemPedido, ItemPedidoViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        }
    }
}
