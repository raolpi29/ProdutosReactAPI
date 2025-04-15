using AutoMapper;
using ProdutosReactAPI.Aplicacao.Dtos.Produtos;
using ProdutosReactAPI.Aplicacao.Dtos.Usuarios;
using ProdutosReactAPI.Dominio.Entidades;

namespace ProdutosReactAPI.Aplicacao.Dtos
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Produto, ProdutoDto>();
            CreateMap<Usuario, UsuarioDto>();
        }
    }
}
