using Microsoft.EntityFrameworkCore;
using ProdutosReactAPI.Dominio.Entidades;
using ProdutosReactAPI.Dominio.Filtros;
using ProdutosReactAPI.Persistencia.Contexto;
using ProdutosReactAPI.Persistencia.Repositorios;

namespace ProdutosReactAPI.Persistencia.Tests.Repositorios
{
    public class ProdutoRepositorioTests
    {
        [Fact]
        public async Task Deve_Adicionar_Produto_Com_Sucesso()
        {
            // Arrange
            var opcoes = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("BancoDeTeste")
                .Options;

            using var contexto = new AppDbContext(opcoes);
            var repositorio = new ProdutoRepositorio(contexto);
            var produto = new Produto { Nome = "ProdutoTeste", Valor = 10.0m };

            // Act
            await repositorio.AdicionarAsync(produto);
            contexto.SaveChanges();

            // Assert
            var produtoAdicionado = contexto.Produtos.FirstOrDefault(p => p.Nome == "ProdutoTeste");
            Assert.NotNull(produtoAdicionado);
            Assert.Equal("ProdutoTeste", produtoAdicionado.Nome);
        }

        [Fact]
        public async Task Deve_Atualizar_Produto_Com_Sucesso()
        {
            // Arrange
            var opcoes = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("BancoDeTesteAtualizar")
                .Options;

            using var contexto = new AppDbContext(opcoes);
            var repositorio = new ProdutoRepositorio(contexto);
            var produto = new Produto { Nome = "ProdutoTeste", Valor = 10.0m };
            await repositorio.AdicionarAsync(produto);
            contexto.SaveChanges();

            // Act
            produto.Nome = "ProdutoAtualizado";
            await repositorio.AtualizarAsync(produto);

            // Assert
            var produtoAtualizado = contexto.Produtos.FirstOrDefault(p => p.IdProduto == produto.IdProduto);
            Assert.NotNull(produtoAtualizado);
            Assert.Equal("ProdutoAtualizado", produtoAtualizado.Nome);
        }

        [Fact]
        public async Task Deve_Excluir_Produto_Com_Sucesso()
        {
            // Arrange
            var opcoes = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("BancoDeTesteExcluir")
                .Options;

            using var contexto = new AppDbContext(opcoes);
            var repositorio = new ProdutoRepositorio(contexto);
            var produto = new Produto { Nome = "ProdutoTeste", Valor = 10.0m };
            await repositorio.AdicionarAsync(produto);
            contexto.SaveChanges();

            // Detach the entity to avoid tracking issues
            contexto.Entry(produto).State = EntityState.Detached;

            // Act
            await repositorio.ExcluirAsync(produto.IdProduto);

            // Assert
            var produtoExcluido = contexto.Produtos.FirstOrDefault(p => p.IdProduto == produto.IdProduto);
            Assert.Null(produtoExcluido);
        }

        [Fact]
        public async Task Deve_Obter_Produto_Por_Id_Com_Sucesso()
        {
            // Arrange
            var opcoes = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("BancoDeTesteObterPorId")
                .Options;

            using var contexto = new AppDbContext(opcoes);
            var repositorio = new ProdutoRepositorio(contexto);
            var produto = new Produto { Nome = "ProdutoTeste", Valor = 10.0m };
            await repositorio.AdicionarAsync(produto);
            contexto.SaveChanges();

            // Act
            var produtoObtido = await repositorio.ObterPorIdAsync(produto.IdProduto);

            // Assert
            Assert.NotNull(produtoObtido);
            Assert.Equal(produto.IdProduto, produtoObtido.IdProduto);
        }

        [Fact]
        public async Task Deve_Obter_Todos_Produtos_Com_Filtro()
        {
            // Arrange
            var opcoes = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("BancoDeTesteObterTodos")
                .Options;

            using var contexto = new AppDbContext(opcoes);
            var repositorio = new ProdutoRepositorio(contexto);
            var produto1 = new Produto { Nome = "ProdutoA", Valor = 10.0m };
            var produto2 = new Produto { Nome = "ProdutoB", Valor = 20.0m };
            await repositorio.AdicionarAsync(produto1);
            await repositorio.AdicionarAsync(produto2);
            contexto.SaveChanges();

            var filtro = new Filtro(1, 10, "nome", false);

            // Act
            var (produtos, total) = await repositorio.ObterTodosAsync(filtro);

            // Assert
            Assert.Equal(2, total);
            Assert.Contains(produtos, p => p.Nome == "ProdutoA");
            Assert.Contains(produtos, p => p.Nome == "ProdutoB");
        }
    }
}