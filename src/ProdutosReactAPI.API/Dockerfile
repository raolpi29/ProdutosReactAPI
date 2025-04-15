# Etapa base da aplicação .NET (usada no final)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000 5001

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Instala dotnet-ef globalmente para gerar migrations durante o build, se necessário
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Copia os arquivos de projeto
COPY ["src/ProdutosReactAPI.API/ProdutosReactAPI.API.csproj", "src/ProdutosReactAPI.API/"]
COPY ["src/ProdutosReactAPI.Aplicacao/ProdutosReactAPI.Aplicacao.csproj", "src/ProdutosReactAPI.Aplicacao/"]
COPY ["src/ProdutosReactAPI.Dominio/ProdutosReactAPI.Dominio.csproj", "src/ProdutosReactAPI.Dominio/"]
COPY ["src/ProdutosReactAPI.Infraestrutura/ProdutosReactAPI.Infraestrutura.csproj", "src/ProdutosReactAPI.Infraestrutura/"]
COPY ["src/ProdutosReactAPI.Persistencia/ProdutosReactAPI.Persistencia.csproj", "src/ProdutosReactAPI.Persistencia/"]

# Restaura dependências
RUN dotnet restore "./src/ProdutosReactAPI.API/ProdutosReactAPI.API.csproj"

# Copia todo o código-fonte
COPY . .

# Compila o projeto
WORKDIR "/src/src/ProdutosReactAPI.API"
RUN dotnet build "ProdutosReactAPI.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ProdutosReactAPI.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final da imagem
FROM base AS final
WORKDIR /app

# Copia os arquivos publicados
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ProdutosReactAPI.API.dll"]