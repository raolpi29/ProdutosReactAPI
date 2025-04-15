using Microsoft.OpenApi.Models;

namespace ProdutosReactAPI.API.Configuracao
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Aplicação Rafael Pinheiro",
                    Version = "v1",
                    Description = "Aplicação Rafael Pinheiro",
                    Contact = new OpenApiContact
                    {
                        Name = "Rafael Pinheiro",
                        Email = "raolpi29@gmail.com"
                    }
                });
            });

            return services;
        }
    }
}
