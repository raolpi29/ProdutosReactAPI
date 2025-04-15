using Microsoft.EntityFrameworkCore;
using ProdutosReactAPI.Persistencia.Contexto;

namespace ProdutosReactAPI.Persistencia.Tests.Contexto
{
    public class AppDbContextTests
    {
        [Fact]
        public void Deve_Criar_DbContext_Com_Sucesso()
        {
            // Arrange
            var opcoes = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("BancoDeTeste")
                .Options;

            // Act
            using var contexto = new AppDbContext(opcoes);

            // Assert
            Assert.NotNull(contexto);
        }
    }
}