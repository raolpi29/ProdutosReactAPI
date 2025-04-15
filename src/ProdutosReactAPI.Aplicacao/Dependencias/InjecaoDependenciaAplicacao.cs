using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProdutosReactAPI.Aplicacao.Dtos;
using ProdutosReactAPI.Aplicacao.Interfaces.Services;
using ProdutosReactAPI.Aplicacao.Services;

namespace ProdutosReactAPI.Aplicacao.Dependencias
{
    public static class InjecaoDependenciaAplicacao
    {
        public static IServiceCollection AdicionarAplicacao(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            return services;
        }
    }
}
