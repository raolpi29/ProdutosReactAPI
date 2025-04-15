using ProdutosReactAPI.Dominio.Entidades;

namespace ProdutosReactAPI.Dominio.Tests.Entidades
{
    public class ProdutoTests
    {
        [Fact]
        public void Produto_DeveSerCriadoComValoresValidos()
        {
            // Arrange
            var nome = "Produto Teste";
            var valor = 100.50m;

            // Act
            var produto = new Produto(nome, valor);

            // Assert
            Assert.Equal(nome, produto.Nome);
            Assert.Equal(valor, produto.Valor);
            Assert.Empty(produto.Notificacoes);
        }

        [Fact]
        public void Produto_DeveAdicionarNotificacao_QuandoNomeForInvalido()
        {
            // Arrange
            var nome = "";
            var valor = 100.50m;

            // Act
            var produto = new Produto(nome, valor);

            // Assert
            Assert.Contains(produto.Notificacoes, n => n.Mensagem == "O nome não pode ser vazio.");
        }

        [Fact]
        public void Produto_DeveAdicionarNotificacao_QuandoValorForInvalido()
        {
            // Arrange
            var nome = "Produto Teste";
            var valor = 0;

            // Act
            var produto = new Produto(nome, valor);

            // Assert
            Assert.Contains(produto.Notificacoes, n => n.Mensagem == "O valor deve ser maior que zero.");
        }
    }
}