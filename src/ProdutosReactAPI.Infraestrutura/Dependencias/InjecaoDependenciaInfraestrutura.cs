using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProdutosReactAPI.Aplicacao.Interfaces.Infraestrutura.Criptografia;
using ProdutosReactAPI.Aplicacao.Interfaces.Infraestrutura.Excel;
using ProdutosReactAPI.Infraestrutura.Criptografia;
using ProdutosReactAPI.Infraestrutura.Excel;

namespace ProdutosReactAPI.Infraestrutura.Dependencias
{
    public static class InjecaoDependenciaInfraestrutura
    {
        public static IServiceCollection AdicionarInfraestrutura(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICriptografiaService, CriptografiaService>();
            services.AddScoped<IExcelService, ExcelService>();

            return services;
        }
    }
}
