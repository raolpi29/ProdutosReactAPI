using ProdutosReactAPI.Aplicacao.Dtos;

namespace ProdutosReactAPI.Aplicacao.Test.Dtos
{
    public class FiltroDtoTests
    {
        [Fact]
        public void DeveConverterParaFiltroCorretamente()
        {
            // Arrange
            var filtroDto = new FiltroDto
            {
                Page = 2,
                PageSize = 20,
                Sort = "Nome",
                SortDescending = true
            };

            // Act
            var filtro = filtroDto.ToFiltro();

            // Assert
            Assert.Equal(filtroDto.Page, filtro.Page);
            Assert.Equal(filtroDto.PageSize, filtro.PageSize);
            Assert.Equal(filtroDto.Sort, filtro.Sort);
            Assert.Equal(filtroDto.SortDescending, filtro.SortDescending);
        }
    }
}