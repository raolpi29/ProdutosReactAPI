using ProdutosReactAPI.Aplicacao.Dtos.Produtos;

namespace ProdutosReactAPI.Aplicacao.Test.Dtos.Produtos
{
    public class CriarProdutoDtoTests
    {
        [Fact]
        public void DeveInicializarComValoresPadrao()
        {
            // Arrange & Act
            var dto = new CriarProdutoDto();

            // Assert
            Assert.Equal(string.Empty, dto.Nome);
            Assert.Equal(0, dto.Valor);
        }
    }
}