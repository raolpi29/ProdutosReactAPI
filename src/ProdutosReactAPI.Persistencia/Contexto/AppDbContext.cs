using Microsoft.EntityFrameworkCore;
using ProdutosReactAPI.Dominio.Entidades;
using ProdutosReactAPI.Dominio.Notifications;
using ProdutosReactAPI.Persistencia.Mapeamentos;

namespace ProdutosReactAPI.Persistencia.Contexto
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<Produto> Produtos => Set<Produto>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notificacao>();

            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
