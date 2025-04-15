using ProdutosReactAPI.Dominio.Filtros;

namespace ProdutosReactAPI.Dominio.Tests.Filtros
{
    public class FiltroTests
    {
        [Fact]
        public void Filtro_DeveSerCriadoComValoresPadrao()
        {
            // Act
            var filtro = new Filtro(1, 10, null, false);

            // Assert
            Assert.Equal(1, filtro.Page);
            Assert.Equal(10, filtro.PageSize);
            Assert.Null(filtro.Sort);
            Assert.False(filtro.SortDescending);
        }

        [Fact]
        public void Filtro_DevePermitirAlterarValores()
        {
            // Arrange
            var filtro = new Filtro(1, 10, null, false);

            // Act
            filtro.Page = 2;
            filtro.PageSize = 20;
            filtro.Sort = "Nome";
            filtro.SortDescending = true;

            // Assert
            Assert.Equal(2, filtro.Page);
            Assert.Equal(20, filtro.PageSize);
            Assert.Equal("Nome", filtro.Sort);
            Assert.True(filtro.SortDescending);
        }
    }
}