using ProdutosReactAPI.Aplicacao.Dtos;
using ProdutosReactAPI.Dominio.Notifications;

namespace ProdutosReactAPI.Aplicacao.Test.Dtos
{
    public class ResultTests
    {
        [Fact]
        public void DeveCriarResultadoComSucesso()
        {
            // Arrange
            var dados = "Teste";

            // Act
            var result = Result<string>.Ok(dados);

            // Assert
            Assert.True(result.Sucesso);
            Assert.Equal(dados, result.Dados);
            Assert.Empty(result.Erros);
        }

        [Fact]
        public void DeveCriarResultadoComFalha()
        {
            // Arrange
            var erros = new List<Notificacao>();
            erros.Add(new Notificacao("Nome", "O nome é obrigatório."));

            // Act
            var result = Result<string>.Falha(erros);

            // Assert
            Assert.False(result.Sucesso);
            Assert.Null(result.Dados);
            Assert.Equal(erros, result.Erros);
        }
    }
}