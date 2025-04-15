using ProdutosReactAPI.Dominio.Entidades;
using ProdutosReactAPI.Dominio.Filtros;

namespace ProdutosReactAPI.Dominio.Contratos.Repositorios
{
    public interface IProdutoRepositorio
    {
        Task<Produto?> ObterPorIdAsync(Guid id);
        Task<(IEnumerable<Produto>, int)> ObterTodosAsync(Filtro filtro);
        Task AdicionarAsync(Produto produto);
        Task AtualizarAsync(Produto produto);
        Task ExcluirAsync(Guid id);
    }
}
