using ProdutosReactAPI.Aplicacao.Dtos.Produtos;

namespace ProdutosReactAPI.Aplicacao.Test.Dtos.Produtos
{
    public class ProdutoDtoTests
    {
        [Fact]
        public void DeveInicializarComValoresPadrao()
        {
            // Arrange & Act
            var dto = new ProdutoDto();

            // Assert
            Assert.Equal(default, dto.IdProduto);
            Assert.Equal(string.Empty, dto.Nome);
            Assert.Equal(0, dto.Valor);
            Assert.Equal(default, dto.DataInclusao);
        }
    }
}