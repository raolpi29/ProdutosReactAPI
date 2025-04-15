using Microsoft.EntityFrameworkCore;
using ProdutosReactAPI.Dominio.Contratos.Repositorios;
using ProdutosReactAPI.Dominio.Entidades;
using ProdutosReactAPI.Persistencia.Contexto;

namespace ProdutosReactAPI.Persistencia.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AppDbContext _context;

        public UsuarioRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObterPorLoginAsync(string login)
        {
            return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<Usuario?> ObterPorIdAsync(Guid id)
        {
            return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }
    }
}