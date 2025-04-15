using ProdutosReactAPI.Aplicacao.Dtos.Usuarios;
using ProdutosReactAPI.Aplicacao.Interfaces.Services;

namespace ProdutosReactAPI.API.Seed
{
    public static class SeedData
    {
        public static async Task InicializarAsync(IServiceProvider serviceProvider)
        {
            var usuarioService = serviceProvider.GetRequiredService<IUsuarioService>();

            var resultado = await usuarioService.CriarAsync(new CriarUsuarioDto
            {
                Login = "admin",
                Senha = "123"
            });

            if (!resultado.Sucesso)
            {
                Console.WriteLine("Erro ao inserir usuário admin no seed:");
                foreach (var erro in resultado.Erros)
                    Console.WriteLine($" - {erro}");
            }
            else
            {
                Console.WriteLine("Usuário admin criado com sucesso.");
            }
        }
    }
}
