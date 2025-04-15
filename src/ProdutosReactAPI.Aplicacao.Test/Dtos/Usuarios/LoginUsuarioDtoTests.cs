using ProdutosReactAPI.Aplicacao.Dtos.Usuarios;

namespace ProdutosReactAPI.Aplicacao.Test.Dtos.Usuarios
{
    public class LoginUsuarioDtoTests
    {
        [Fact]
        public void DeveInicializarComValoresPadrao()
        {
            // Arrange & Act
            var dto = new LoginUsuarioDto();

            // Assert
            Assert.Equal(string.Empty, dto.Login);
            Assert.Equal(string.Empty, dto.Senha);
        }
    }
}