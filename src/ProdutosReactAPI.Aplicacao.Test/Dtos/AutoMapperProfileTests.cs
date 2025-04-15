using AutoMapper;
using ProdutosReactAPI.Aplicacao.Dtos;
using ProdutosReactAPI.Aplicacao.Dtos.Produtos;
using ProdutosReactAPI.Aplicacao.Dtos.Usuarios;
using ProdutosReactAPI.Dominio.Entidades;

namespace ProdutosReactAPI.Aplicacao.Test.Dtos
{
    public class AutoMapperProfileTests
    {
        private readonly IMapper _mapper;

        public AutoMapperProfileTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void DeveMapearProdutoParaProdutoDto()
        {
            // Arrange
            var produto = new Produto { IdProduto = Guid.NewGuid(), Nome = "Produto Teste" };

            // Act
            var produtoDto = _mapper.Map<ProdutoDto>(produto);

            // Assert
            Assert.Equal(produto.IdProduto, produtoDto.IdProduto);
            Assert.Equal(produto.Nome, produtoDto.Nome);
        }

        [Fact]
        public void DeveMapearUsuarioParaUsuarioDto()
        {
            // Arrange
            var usuario = new Usuario { Login = "Usuario Teste", DataInclusao = DateTime.UtcNow };

            // Act
            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            // Assert
            Assert.Equal(usuario.Login, usuarioDto.Login);
            Assert.Equal(usuario.DataInclusao, usuarioDto.DataInclusao);
        }
    }
}