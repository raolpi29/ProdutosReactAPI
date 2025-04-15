using Microsoft.EntityFrameworkCore;
using ProdutosReactAPI.Dominio.Entidades;
using ProdutosReactAPI.Persistencia.Contexto;

namespace ProdutosReactAPI.Persistencia.Tests.Mapeamentos
{
    public class UsuarioMapTests
    {
        [Fact]
        public void Deve_Configurar_Mapeamento_Usuario_Corretamente()
        {
            // Arrange
            var opcoes = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("BancoDeTeste")
                .Options;

            using var contexto = new AppDbContext(opcoes);

            // Act
            var entidade = contexto.Model.FindEntityType(typeof(Usuario));

            // Assert
            Assert.NotNull(entidade);
            Assert.Equal("Usuarios", entidade.GetTableName());
            Assert.Contains(entidade.GetProperties(), p => p.Name == "IdUsuario" && p.IsPrimaryKey());
            Assert.Contains(entidade.GetProperties(), p => p.Name == "Login" && p.GetMaxLength() == 50);
            Assert.Contains(entidade.GetProperties(), p => p.Name == "Senha" && p.IsNullable == false);
            Assert.Contains(entidade.GetProperties(), p => p.Name == "DataInclusao" && p.IsNullable == false);
        }
    }
}