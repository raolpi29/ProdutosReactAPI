using ProdutosReactAPI.Aplicacao.Dtos.Usuarios;

namespace ProdutosReactAPI.Aplicacao.Test.Dtos.Usuarios
{
    public class UsuarioDtoTests
    {
        [Fact]
        public void DeveInicializarComValoresPadrao()
        {
            // Arrange & Act
            var dto = new UsuarioDto();

            // Assert
            Assert.Equal(string.Empty, dto.Login);
            Assert.Equal(default, dto.DataInclusao);
        }
    }
}