using ProdutosReactAPI.Aplicacao.Dtos;
using ProdutosReactAPI.Aplicacao.Dtos.Usuarios;

namespace ProdutosReactAPI.Aplicacao.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<Result<UsuarioDto>> CriarAsync(CriarUsuarioDto dto);

        Task<Result<UsuarioDto>> LoginAsync(LoginUsuarioDto dto);
    }
}
