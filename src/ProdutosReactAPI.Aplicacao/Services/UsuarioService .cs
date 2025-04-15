using AutoMapper;
using ProdutosReactAPI.Aplicacao.Dtos;
using ProdutosReactAPI.Aplicacao.Dtos.Usuarios;
using ProdutosReactAPI.Aplicacao.Interfaces.Infraestrutura.Criptografia;
using ProdutosReactAPI.Aplicacao.Interfaces.Services;
using ProdutosReactAPI.Dominio.Contratos.Repositorios;
using ProdutosReactAPI.Dominio.Entidades;

namespace ProdutosReactAPI.Aplicacao.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly ICriptografiaService _criptografia;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepositorio repositorio, ICriptografiaService criptografia, IMapper mapper)
        {
            _repositorio = repositorio;
            _criptografia = criptografia;
            _mapper = mapper;
        }

        public async Task<Result<UsuarioDto>> CriarAsync(CriarUsuarioDto dto)
        {
            var senhaHash = _criptografia.Hash(dto.Senha);
            var usuario = new Usuario(dto.Login, senhaHash);

            if (!usuario.EhValido)
                return Result<UsuarioDto>.Falha(usuario.Notificacoes);

            var usuarioExistente = await _repositorio.ObterPorLoginAsync(dto.Login);
            if (usuarioExistente != null)
            {
                usuario.AdicionarNotificacao(nameof(Usuario), "Já existe um usuário com esse login.");
                return Result<UsuarioDto>.Falha(usuario.Notificacoes);
            }

            await _repositorio.AdicionarAsync(usuario);

            return Result<UsuarioDto>.Ok(_mapper.Map<UsuarioDto>(usuario));
        }

        public async Task<Result<UsuarioDto>> LoginAsync(LoginUsuarioDto dto)
        {
            var usuario = await _repositorio.ObterPorLoginAsync(dto.Login);

            if (usuario is null)
                return Result<UsuarioDto>.Falha([new("Usuario", "Usuário ou senha inválidos.")]);

            var senhaValida = _criptografia.Verificar(dto.Senha, usuario.Senha);

            if (!senhaValida)
                return Result<UsuarioDto>.Falha([new("Usuario", "Usuário ou senha inválidos.")]);

            return Result<UsuarioDto>.Ok(_mapper.Map<UsuarioDto>(usuario));
        }
    }
}
