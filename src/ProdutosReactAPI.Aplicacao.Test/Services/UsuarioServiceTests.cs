using AutoMapper;
using Moq;
using ProdutosReactAPI.Aplicacao.Dtos.Usuarios;
using ProdutosReactAPI.Aplicacao.Interfaces.Infraestrutura.Criptografia;
using ProdutosReactAPI.Aplicacao.Services;
using ProdutosReactAPI.Dominio.Contratos.Repositorios;
using ProdutosReactAPI.Dominio.Entidades;

namespace ProdutosReactAPI.Aplicacao.Test.Services
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepositorio> _repositorioMock;
        private readonly Mock<ICriptografiaService> _criptografiaMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UsuarioService _usuarioService;

        public UsuarioServiceTests()
        {
            _repositorioMock = new Mock<IUsuarioRepositorio>();
            _criptografiaMock = new Mock<ICriptografiaService>();
            _mapperMock = new Mock<IMapper>();
            _usuarioService = new UsuarioService(_repositorioMock.Object, _criptografiaMock.Object, _mapperMock.Object);
        }

        [Fact(DisplayName = "Deve criar usuário com sucesso")]
        public async Task DeveCriarUsuarioComSucesso()
        {
            // Arrange
            var criarUsuarioDto = new CriarUsuarioDto { Login = "usuario", Senha = "senha" };
            var senhaHash = "senhaHash";
            var usuarioCriado = new Usuario(criarUsuarioDto.Login, senhaHash);

            _criptografiaMock.Setup(c => c.Hash(criarUsuarioDto.Senha)).Returns(senhaHash);
            _repositorioMock.Setup(r => r.ObterPorLoginAsync(criarUsuarioDto.Login)).ReturnsAsync((Usuario)null);

            Usuario usuarioSalvo = null!;
            _repositorioMock
                .Setup(r => r.AdicionarAsync(It.IsAny<Usuario>()))
                .Callback<Usuario>(u => usuarioSalvo = u)
                .Returns(Task.CompletedTask);

            _mapperMock
                .Setup(m => m.Map<UsuarioDto>(It.IsAny<Usuario>()))
                .Returns((Usuario u) => new UsuarioDto { Login = u.Login });

            // Act
            var result = await _usuarioService.CriarAsync(criarUsuarioDto);

            // Assert
            Assert.True(result.Sucesso);
            Assert.NotNull(result.Dados);
            Assert.Equal(criarUsuarioDto.Login, result.Dados.Login);
            _repositorioMock.Verify(r => r.AdicionarAsync(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public async Task DeveRetornarFalhaAoCriarUsuarioComLoginExistente()
        {
            // Arrange
            var criarUsuarioDto = new CriarUsuarioDto { Login = "usuario", Senha = "senha" };
            var usuarioExistente = new Usuario(criarUsuarioDto.Login, "senhaHash");
            _repositorioMock.Setup(r => r.ObterPorLoginAsync(criarUsuarioDto.Login)).ReturnsAsync(usuarioExistente);

            // Act
            var result = await _usuarioService.CriarAsync(criarUsuarioDto);

            // Assert
            Assert.False(result.Sucesso);
            Assert.NotEmpty(result.Erros);
            _repositorioMock.Verify(r => r.AdicionarAsync(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact]
        public async Task DeveRealizarLoginComSucesso()
        {
            // Arrange
            var loginUsuarioDto = new LoginUsuarioDto { Login = "usuario", Senha = "senha" };
            var usuario = new Usuario(loginUsuarioDto.Login, "senhaHash");
            _repositorioMock.Setup(r => r.ObterPorLoginAsync(loginUsuarioDto.Login)).ReturnsAsync(usuario);
            _criptografiaMock.Setup(c => c.Verificar(loginUsuarioDto.Senha, usuario.Senha)).Returns(true);
            _mapperMock.Setup(m => m.Map<UsuarioDto>(usuario)).Returns(new UsuarioDto { Login = usuario.Login });

            // Act
            var result = await _usuarioService.LoginAsync(loginUsuarioDto);

            // Assert
            Assert.True(result.Sucesso);
            Assert.Equal(loginUsuarioDto.Login, result.Dados.Login);
        }

        [Fact]
        public async Task DeveRetornarFalhaAoRealizarLoginComSenhaInvalida()
        {
            // Arrange
            var loginUsuarioDto = new LoginUsuarioDto { Login = "usuario", Senha = "senha" };
            var usuario = new Usuario(loginUsuarioDto.Login, "senhaHash");
            _repositorioMock.Setup(r => r.ObterPorLoginAsync(loginUsuarioDto.Login)).ReturnsAsync(usuario);
            _criptografiaMock.Setup(c => c.Verificar(loginUsuarioDto.Senha, usuario.Senha)).Returns(false);

            // Act
            var result = await _usuarioService.LoginAsync(loginUsuarioDto);

            // Assert
            Assert.False(result.Sucesso);
            Assert.NotEmpty(result.Erros);
        }
    }
}