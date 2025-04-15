using Microsoft.EntityFrameworkCore;
using ProdutosReactAPI.Dominio.Entidades;
using ProdutosReactAPI.Persistencia.Contexto;

namespace ProdutosReactAPI.Persistencia.Tests.Mapeamentos
{
    public class ProdutoMapTests
    {
        [Fact]
        public void Deve_Configurar_Mapeamento_Produto_Corretamente()
        {
            // Arrange
            var opcoes = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("BancoDeTeste")
                .Options;

            using var contexto = new AppDbContext(opcoes);

            // Act
            var entidade = contexto.Model.FindEntityType(typeof(Produto));

            // Assert
            Assert.NotNull(entidade);
            Assert.Equal("Produtos", entidade.GetTableName());
        }
    }
}