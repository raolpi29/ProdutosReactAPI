using Moq;
using ProdutosReactAPI.Aplicacao.Dtos;
using ProdutosReactAPI.Aplicacao.Dtos.Produtos;
using ProdutosReactAPI.Aplicacao.Interfaces.Services;

namespace ProdutosReactAPI.Aplicacao.Test.Interfaces.Services
{
    public class IProdutoServiceTests
    {
        [Fact]
        public async Task DeveChamarCriarAsync()
        {
            // Arrange
            var mockService = new Mock<IProdutoService>();
            var dto = new CriarProdutoDto { Nome = "Produto Teste", Valor = 100 };
            mockService.Setup(s => s.CriarAsync(dto)).ReturnsAsync(Result<ProdutoDto>.Ok(new ProdutoDto()));

            // Act
            var result = await mockService.Object.CriarAsync(dto);

            // Assert
            mockService.Verify(s => s.CriarAsync(dto), Times.Once);
            Assert.True(result.Sucesso);
        }
    }
}