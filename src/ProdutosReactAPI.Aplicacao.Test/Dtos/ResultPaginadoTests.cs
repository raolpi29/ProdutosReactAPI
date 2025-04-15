using ProdutosReactAPI.Aplicacao.Dtos;
using ProdutosReactAPI.Dominio.Notifications;

namespace ProdutosReactAPI.Aplicacao.Test.Dtos
{
    public class ResultPaginadoTests
    {
        [Fact]
        public void DeveCriarResultadoPaginadoComSucesso()
        {
            // Arrange
            var dados = new Paginado<string> { Itens = new[] { "Item1", "Item2" } };

            // Act
            var result = ResultPaginado<string>.Ok(dados);

            // Assert
            Assert.True(result.Sucesso);
            Assert.Equal(dados, result.Dados);
            Assert.Empty(result.Erros);
        }

        [Fact]
        public void DeveCriarResultadoPaginadoComFalha()
        {
            // Arrange
            var erros = new List<Notificacao>();
            erros.Add(new Notificacao("Erro1", "Mensagem de erro 1"));

            // Act
            var result = ResultPaginado<string>.Falha(erros);

            // Assert
            Assert.False(result.Sucesso);
            Assert.Null(result.Dados);
            Assert.Equal(erros, result.Erros);
        }
    }
}