using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProdutosReactAPI.Dominio.Contratos.Repositorios;
using ProdutosReactAPI.Persistencia.Contexto;
using ProdutosReactAPI.Persistencia.Repositorios;

namespace ProdutosReactAPI.Persistencia.Depencias
{
    public static class InjecaoDependenciaPersistencia
    {
        public static IServiceCollection AdicionarPersistencia(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsAssembly("ProdutosReactAPI.Persistencia")
                )
            );

            services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

            return services;
        }
    }
}
