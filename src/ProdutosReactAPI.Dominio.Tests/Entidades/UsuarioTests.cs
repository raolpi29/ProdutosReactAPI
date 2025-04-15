using ProdutosReactAPI.Dominio.Entidades;

namespace ProdutosReactAPI.Dominio.Tests.Entidades
{
    public class UsuarioTests
    {
        [Fact]
        public void Usuario_DeveSerCriadoComValoresValidos()
        {
            // Arrange
            var login = "usuario.teste";
            var senha = "senha123";

            // Act
            var usuario = new Usuario(login, senha);

            // Assert
            Assert.Equal(login, usuario.Login);
            Assert.Equal(senha, usuario.Senha);
            Assert.Empty(usuario.Notificacoes);
        }

        [Fact]
        public void Usuario_DeveAdicionarNotificacao_QuandoLoginForInvalido()
        {
            // Arrange
            var login = "";
            var senha = "senha123";

            // Act
            var usuario = new Usuario(login, senha);

            // Assert
            Assert.Contains(usuario.Notificacoes, n => n.Mensagem == "O login não pode ser vazio.");
        }

        [Fact]
        public void Usuario_DeveAdicionarNotificacao_QuandoSenhaForInvalida()
        {
            // Arrange
            var login = "usuario.teste";
            var senha = "";

            // Act
            var usuario = new Usuario(login, senha);

            // Assert
            Assert.Contains(usuario.Notificacoes, n => n.Mensagem == "A senha não pode ser vazia.");
        }
    }
}