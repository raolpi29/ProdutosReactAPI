using ProdutosReactAPI.Aplicacao.Dtos.Usuarios;

namespace ProdutosReactAPI.Aplicacao.Test.Dtos.Usuarios
{
    public class CriarUsuarioDtoTests
    {
        [Fact]
        public void DeveInicializarComValoresPadrao()
        {
            // Arrange & Act
            var dto = new CriarUsuarioDto();

            // Assert
            Assert.Equal(string.Empty, dto.Login);
            Assert.Equal(string.Empty, dto.Senha);
        }
    }
}