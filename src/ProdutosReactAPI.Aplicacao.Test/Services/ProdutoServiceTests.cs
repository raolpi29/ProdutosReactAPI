using AutoMapper;
using Moq;
using ProdutosReactAPI.Aplicacao.Dtos;
using ProdutosReactAPI.Aplicacao.Dtos.Produtos;
using ProdutosReactAPI.Aplicacao.Interfaces.Infraestrutura.Excel;
using ProdutosReactAPI.Aplicacao.Services;
using ProdutosReactAPI.Dominio.Contratos.Repositorios;
using ProdutosReactAPI.Dominio.Entidades;

namespace ProdutosReactAPI.Aplicacao.Test.Services
{
    public class ProdutoServiceTests
    {
        private readonly Mock<IProdutoRepositorio> _repositorioMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IExcelService> _excelServiceMock;
        private readonly ProdutoService _produtoService;

        public ProdutoServiceTests()
        {
            _repositorioMock = new Mock<IProdutoRepositorio>();
            _mapperMock = new Mock<IMapper>();
            _excelServiceMock = new Mock<IExcelService>();
            _produtoService = new ProdutoService(_repositorioMock.Object, _mapperMock.Object, _excelServiceMock.Object);
        }

        [Fact]
        public async Task DeveObterProdutoPorIdComSucesso()
        {
            // Arrange
            var produtoId = Guid.NewGuid();
            var produto = new Produto("Produto Teste", 100) { IdProduto = produtoId };
            _repositorioMock.Setup(r => r.ObterPorIdAsync(produtoId)).ReturnsAsync(produto);
            _mapperMock.Setup(m => m.Map<ProdutoDto>(produto)).Returns(new ProdutoDto { Nome = produto.Nome, Valor = produto.Valor });

            // Act
            var result = await _produtoService.ObterPorIdAsync(produtoId);

            // Assert
            Assert.True(result.Sucesso);
            Assert.Equal(produto.Nome, result.Dados.Nome);
            Assert.Equal(produto.Valor, result.Dados.Valor);
            _repositorioMock.Verify(r => r.ObterPorIdAsync(produtoId), Times.Once);
        }

        [Fact]
        public async Task DeveRetornarFalhaQuandoProdutoNaoEncontradoPorId()
        {
            // Arrange
            var produtoId = Guid.NewGuid();
            _repositorioMock.Setup(r => r.ObterPorIdAsync(produtoId)).ReturnsAsync((Produto)null);

            // Act
            var result = await _produtoService.ObterPorIdAsync(produtoId);

            // Assert
            Assert.False(result.Sucesso);
            Assert.Null(result.Dados);
            Assert.NotEmpty(result.Erros);
            _repositorioMock.Verify(r => r.ObterPorIdAsync(produtoId), Times.Once);
        }

        [Fact]
        public async Task DeveExcluirProdutoComSucesso()
        {
            // Arrange
            var produtoId = Guid.NewGuid();
            var produto = new Produto("Produto Teste", 100) { IdProduto = produtoId };
            _repositorioMock.Setup(r => r.ObterPorIdAsync(produtoId)).ReturnsAsync(produto);
            _repositorioMock.Setup(r => r.ExcluirAsync(produtoId)).Returns(Task.CompletedTask);

            // Act
            var result = await _produtoService.ExcluirAsync(produtoId);

            // Assert
            Assert.True(result.Sucesso);
            Assert.True(result.Dados);
            _repositorioMock.Verify(r => r.ObterPorIdAsync(produtoId), Times.Once);
            _repositorioMock.Verify(r => r.ExcluirAsync(produtoId), Times.Once);
        }

        [Fact]
        public async Task DeveRetornarFalhaAoExcluirProdutoNaoEncontrado()
        {
            // Arrange
            var produtoId = Guid.NewGuid();
            _repositorioMock.Setup(r => r.ObterPorIdAsync(produtoId)).ReturnsAsync((Produto)null);

            // Act
            var result = await _produtoService.ExcluirAsync(produtoId);

            // Assert
            Assert.False(result.Sucesso);
            Assert.False(result.Dados);
            Assert.NotEmpty(result.Erros);
            _repositorioMock.Verify(r => r.ObterPorIdAsync(produtoId), Times.Once);
            _repositorioMock.Verify(r => r.ExcluirAsync(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task DeveObterTodosProdutosComSucesso()
        {
            // Arrange
            var filtroDto = new FiltroDto { Page = 1, PageSize = 10 };
            var produtos = new List<Produto> { new Produto("Produto Teste", 100) };
            _repositorioMock.Setup(r => r.ObterTodosAsync(It.IsAny<Dominio.Filtros.Filtro>())).ReturnsAsync((produtos, produtos.Count));
            _mapperMock.Setup(m => m.Map<IEnumerable<ProdutoDto>>(produtos)).Returns(new List<ProdutoDto> { new ProdutoDto { Nome = "Produto Teste", Valor = 100 } });

            // Act
            var result = await _produtoService.ObterTodosAsync(filtroDto);

            // Assert
            Assert.True(result.Sucesso);
            Assert.NotEmpty(result.Dados.Itens);
            Assert.Equal(1, result.Dados.TotalItens);
            _repositorioMock.Verify(r => r.ObterTodosAsync(It.IsAny<Dominio.Filtros.Filtro>()), Times.Once);
        }

        [Fact]
        public async Task DeveExportarProdutosParaExcelComSucesso()
        {
            // Arrange
            var produtos = new List<Produto> { new Produto("Produto Teste", 100) };
            var produtoDtos = new List<ProdutoDto> { new ProdutoDto { Nome = "Produto Teste", Valor = 100 } };
            _repositorioMock.Setup(r => r.ObterTodosAsync(It.IsAny<Dominio.Filtros.Filtro>())).ReturnsAsync((produtos, produtos.Count));
            _mapperMock.Setup(m => m.Map<IEnumerable<ProdutoDto>>(produtos)).Returns(produtoDtos);
            _excelServiceMock.Setup(e => e.GerarExcel(produtoDtos, "Produtos")).Returns(new byte[] { 1, 2, 3 });

            // Act
            var result = await _produtoService.ExportarProdutosParaExcelAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            _repositorioMock.Verify(r => r.ObterTodosAsync(It.IsAny<Dominio.Filtros.Filtro>()), Times.Once);
            _excelServiceMock.Verify(e => e.GerarExcel(produtoDtos, "Produtos"), Times.Once);
        }
    }
}