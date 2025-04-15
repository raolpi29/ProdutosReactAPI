using ProdutosReactAPI.Aplicacao.Dtos;
using ProdutosReactAPI.Aplicacao.Dtos.Produtos;

namespace ProdutosReactAPI.Aplicacao.Interfaces.Services
{
    public interface IProdutoService
    {
        Task<Result<ProdutoDto>> CriarAsync(CriarProdutoDto dto);
        Task<Result<ProdutoDto>> ObterPorIdAsync(Guid id);
        Task<ResultPaginado<ProdutoDto>> ObterTodosAsync(FiltroDto filtroDto);
        Task<Result<ProdutoDto>> AtualizarAsync(Guid id, CriarProdutoDto dto);
        Task<Result<bool>> ExcluirAsync(Guid id);
        Task<byte[]> ExportarProdutosParaExcelAsync();

    }
}
