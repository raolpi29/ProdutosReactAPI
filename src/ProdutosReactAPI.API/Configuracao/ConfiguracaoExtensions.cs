namespace ProdutosReactAPI.API.Configuracao
{
    internal static class ConfiguracaoExtensions
    {
        public static IHostBuilder AddConfiguration(this IHostBuilder host)
        {
            string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            host.ConfigureAppConfiguration((builderContext, configBuilder) =>
            {
                configBuilder.AddJsonFile(
                    path: "appsettings.json",
                    optional: false,
                    reloadOnChange: true);

                configBuilder.AddJsonFile(
                    path: $"appsettings.{environment}.json",
                    optional: true,
                    reloadOnChange: true);

                configBuilder.AddEnvironmentVariables();
            });

            return host;
        }
    }
}
