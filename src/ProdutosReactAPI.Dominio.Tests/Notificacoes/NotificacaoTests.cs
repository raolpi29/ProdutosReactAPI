using ProdutosReactAPI.Dominio.Notifications;

namespace ProdutosReactAPI.Dominio.Tests.Notificacoes
{
    public class NotificacaoTests
    {
        [Fact]
        public void Notificacao_DeveSerCriadaComValoresValidos()
        {
            // Arrange
            var propriedade = "Nome";
            var mensagem = "O nome é obrigatório.";

            // Act
            var notificacao = new Notificacao(propriedade, mensagem);

            // Assert
            Assert.Equal(propriedade, notificacao.Propriedade);
            Assert.Equal(mensagem, notificacao.Mensagem);
        }
    }

    public class NotificavelTests
    {
        [Fact]
        public void Notificavel_DeveAdicionarNotificacao()
        {
            // Arrange
            var notificavel = new TesteNotificavel();

            // Act
            notificavel.AdicionarNotificacao("Nome", "O nome é obrigatório.");

            // Assert
            Assert.Single(notificavel.Notificacoes);
            Assert.False(notificavel.EhValido);
        }

        [Fact]
        public void Notificavel_DeveLimparNotificacoes()
        {
            // Arrange
            var notificavel = new TesteNotificavel();
            notificavel.AdicionarNotificacao("Nome", "O nome é obrigatório.");

            // Act
            notificavel.LimparNotificacoes();

            // Assert
            Assert.Empty(notificavel.Notificacoes);
            Assert.True(notificavel.EhValido);
        }

        private class TesteNotificavel : Notificavel { }
    }
}