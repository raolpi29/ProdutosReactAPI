# React + API 

Sistema simples para cadastro de produto

Considerações: 

> A tela de login foi feito apenas uma validação da informação na API(Usuário e Senha) e um redirecionamento para os produtos, não foi feito nenhum controle de usuário logado e sessão no front nem no backend

> Normalmente tenho o costume de produzir um domínio rico, alguns itens que podem ser abordados:
-  Eventos de dominio - Poderiamos publicar um evento para guardar histórico de alteração do produto, no qual posteriormente em uma camada de infra, teriamos um projeto para processar os eventos recebidos junto com um consumidor, que poderiamos utilizar um RabbitMQ, ou até mesmo um evento para sinalizar que o produto foi criado, etc.
- Serviços de dominio - Como por exemplo algum contador de produtos próximo ao vencimento(caso tivesse uma data de vencimento do produto)
- ValueObject - Não abordei esse cenário pois no meu entendimento não existia um requisito para implementação do mesmo
- Notificações - Pude demonstrar um pouco da abordagem de notificações

> Testes - Produzi alguns testes simples no backend, sem cobertura no front, mas normalmente costumo utilizar o Jest. Outra abordagem que costumo utilizar é uma extensão no Visual Studio que chama Fine Code Coverage, ela é muito útil para que você veja os pontos que não estão cobertos.
---

## Estrutura de Pastas

Normalmente eu gosto de separar front e backend em dois repositórios diferentes, porém para este cenário coloquei em ambos separando apenas por pasta.

---

## Instrução  

1. Abrir o PowerShell como administrador.
 > Instalar a versão 7+ [PowerShell](https://github.com/PowerShell/PowerShell)
2. Para instalar o projeto, execute os seguintes comandos:

```powershell
cd <local do projeto>
```

2.1 Criação inicial do migrations

```powershell
dotnet ef migrations add Initial --project src/ProdutosReactAPI.Persistencia --startup-project src/ProdutosReactAPI.API
```

3. Subindo os containers (SQL Server + Aplicação)

Executar o comando para subir o SQL Server

```powershell
docker compose up --build -d 
```

Verifique se o container está rodando:

```powershell
docker ps
```

> **IMPORTANTE:** Caso algum container apresente problemas, verificar o log.

```powershell
docker logs -f <nome do container>
```

> **IMPORTANTE:** Verificar se o migrations foi executado corretamente, por default está sendo criado o usuário admin

```powershell
docker exec -it sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "r@f@eL-123!" -d ProdutosReact -C -Q "SELECT * FROM __EFMigrationsHistory"
```

4. Acessar o swagger da API

> [Swagger API](http://localhost:5000/swagger)

5. Acessar a aplicação

> [Aplicação](http://localhost:3000)
---

## Usuário Admin (Seed da Aplicação)

Durante a execução do `migrations`, a aplicação já cria automaticamente um **usuário administrador padrão** na base de dados, como parte do processo de **Seed**.

### Dados de Acesso Padrão

- **Usuário:** `admin`  
- **Senha:** `123`

> Esses dados podem ser utilizados para autenticação inicial e testes da aplicação.

> **IMPORTANTE:** A criação do usuário foi passado pela service, pois necessita de criptografia dos dados, por isso não será disponibilizado script com a criação do mesmo.
---