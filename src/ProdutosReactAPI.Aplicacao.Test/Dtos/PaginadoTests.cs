using ProdutosReactAPI.Aplicacao.Dtos;

namespace ProdutosReactAPI.Aplicacao.Test.Dtos
{
    public class PaginadoTests
    {
        [Fact]
        public void DeveCalcularTotalPaginasCorretamente()
        {
            // Arrange
            var paginado = new Paginado<string>
            {
                TotalItens = 45,
                TamanhoPagina = 10
            };

            // Act
            var totalPaginas = paginado.TotalPaginas;

            // Assert
            Assert.Equal(5, totalPaginas);
        }
    }
}