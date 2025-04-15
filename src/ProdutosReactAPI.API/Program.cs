using ProdutosReactAPI.API.Configuracao;
using ProdutosReactAPI.Infraestrutura.Dependencias;
using ProdutosReactAPI.Persistencia.Depencias;
using ProdutosReactAPI.Aplicacao.Dependencias;
using System.Text.Json;
using Asp.Versioning;
using ProdutosReactAPI.API.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddConfiguration();

builder.Services
    .AdicionarPersistencia(builder.Configuration)
    .AdicionarAplicacao(builder.Configuration)
    .AdicionarInfraestrutura(builder.Configuration);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwagger(builder.Configuration)
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new HeaderApiVersionReader("version");
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

app.UseCors(cors =>
{
    cors.WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader();
});

await app.UseSeedDataAsync();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
