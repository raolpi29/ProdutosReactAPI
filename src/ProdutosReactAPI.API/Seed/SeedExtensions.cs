using Microsoft.EntityFrameworkCore;
using ProdutosReactAPI.Persistencia.Contexto;

namespace ProdutosReactAPI.API.Seed
{
    public static class SeedExtensions
    {
        public static async Task<IApplicationBuilder> UseSeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<AppDbContext>();
                await context.Database.MigrateAsync();

                await SeedData.InicializarAsync(serviceProvider);
            }
            return app;
        }
    }
}
