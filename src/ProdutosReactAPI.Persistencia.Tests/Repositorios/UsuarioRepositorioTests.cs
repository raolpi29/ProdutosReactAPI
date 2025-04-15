using Microsoft.EntityFrameworkCore;
using ProdutosReactAPI.Dominio.Entidades;
using ProdutosReactAPI.Persistencia.Contexto;
using ProdutosReactAPI.Persistencia.Repositorios;

namespace ProdutosReactAPI.Persistencia.Tests.Repositorios
{
    public class UsuarioRepositorioTests
    {
        [Fact]
        public async Task Deve_Obter_Usuario_Por_Login_Com_Sucesso()
        {
            // Arrange
            var opcoes = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("BancoDeTeste")
                .Options;

            using var contexto = new AppDbContext(opcoes);
            var repositorio = new UsuarioRepositorio(contexto);
            var usuario = new Usuario { IdUsuario = Guid.NewGuid(), Login = "usuarioTeste", Senha = "senha123" };
            await contexto.Usuarios.AddAsync(usuario);
            await contexto.SaveChangesAsync();

            // Act
            var usuarioObtido = await repositorio.ObterPorLoginAsync("usuarioTeste");

            // Assert
            Assert.NotNull(usuarioObtido);
            Assert.Equal("usuarioTeste", usuarioObtido.Login);
        }

        [Fact]
        public async Task Deve_Obter_Usuario_Por_Id_Com_Sucesso()
        {
            // Arrange
            var opcoes = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("BancoDeTeste")
                .Options;

            using var contexto = new AppDbContext(opcoes);
            var repositorio = new UsuarioRepositorio(contexto);
            var usuario = new Usuario { IdUsuario = Guid.NewGuid(), Login = "usuarioTeste", Senha = "senha123" };
            await contexto.Usuarios.AddAsync(usuario);
            await contexto.SaveChangesAsync();

            // Act
            var usuarioObtido = await repositorio.ObterPorIdAsync(usuario.IdUsuario);

            // Assert
            Assert.NotNull(usuarioObtido);
            Assert.Equal(usuario.IdUsuario, usuarioObtido.IdUsuario);
        }

        [Fact]
        public async Task Deve_Adicionar_Usuario_Com_Sucesso()
        {
            // Arrange
            var opcoes = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("BancoDeTeste")
                .Options;

            using var contexto = new AppDbContext(opcoes);
            var repositorio = new UsuarioRepositorio(contexto);
            var usuario = new Usuario { IdUsuario = Guid.NewGuid(), Login = "novoUsuario", Senha = "senha123" };

            // Act
            await repositorio.AdicionarAsync(usuario);

            // Assert
            var usuarioAdicionado = await contexto.Usuarios.FirstOrDefaultAsync(u => u.Login == "novoUsuario");
            Assert.NotNull(usuarioAdicionado);
            Assert.Equal("novoUsuario", usuarioAdicionado.Login);
        }
    }
}