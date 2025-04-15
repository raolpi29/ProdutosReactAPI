using ProdutosReactAPI.Dominio.Entidades;

namespace ProdutosReactAPI.Dominio.Contratos.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario?> ObterPorLoginAsync(string login);
        Task<Usuario?> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Usuario usuario);
    }
}
