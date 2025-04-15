using Moq;
using ProdutosReactAPI.Aplicacao.Dtos;
using ProdutosReactAPI.Aplicacao.Dtos.Usuarios;
using ProdutosReactAPI.Aplicacao.Interfaces.Services;

namespace ProdutosReactAPI.Aplicacao.Test.Interfaces.Services
{
    public class IUsuarioServiceTests
    {
        [Fact]
        public async Task DeveChamarLoginAsync()
        {
            // Arrange
            var mockService = new Mock<IUsuarioService>();
            var dto = new LoginUsuarioDto { Login = "usuario", Senha = "senha" };
            mockService.Setup(s => s.LoginAsync(dto)).ReturnsAsync(Result<UsuarioDto>.Ok(new UsuarioDto()));

            // Act
            var result = await mockService.Object.LoginAsync(dto);

            // Assert
            mockService.Verify(s => s.LoginAsync(dto), Times.Once);
            Assert.True(result.Sucesso);
        }
    }
}