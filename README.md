# CRUD - Catálogo de Produtos

Este é um projeto de exemplo que implementa um CRUD de produtos e categorias usando ASP.NET Core MVC, ASP.NET Core Web API, Entity Framework Core e SQL Server.

## Pré-requisitos

Antes de começar, verifique se o seguinte software está instalado em sua máquina:

- [.NET SDK](https://dotnet.microsoft.com/download) (v7.0 ou posterior)
- [Visual Studio](https://visualstudio.microsoft.com/downloads/) ou [Visual Studio Code](https://code.visualstudio.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Clonando ou Baixando o Repositório

Você pode clonar o repositório usando Git ou fazer o download como um arquivo .zip:

### Clonando com Git

1. Abra o Git Bash ou o terminal de sua escolha.
2. Navegue até o diretório onde deseja clonar o projeto.
3. Execute o seguinte comando:

git clone https://github.com/KenzoZM/CleanArchMvc.git


### Baixando como Arquivo .zip

1. [![Baixar como .zip](https://img.shields.io/badge/Baixar%20como%20.zip-007EC6?style=flat-square&logo=github)](https://github.com/KenzoZM/CleanArchMvc/archive/refs/heads/main.zip)
2. Extraia o conteúdo do arquivo .zip para a pasta desejada em seu computador.


## Configurando o Projeto no Visual Studio

Para configurar e executar o projeto no Visual Studio, siga estas etapas:

1. Abra o arquivo `CleanArchMvc` no Visual Studio.

2. Certifique-se de configurar a string de conexão com o seu banco de dados SQL Server no arquivo `appsettings.json` tanto na camada WebUI como na API
e aplicar a string de conexão no DepencyInjection.cs dentro da camada CleamArchMvc.Infra.Ioc.

Crie um arquivo de texto e nome-o `appsettings.json`, Segue o código de exemplo.

Exemplo Camada WebUI:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=seuServidor;Database=nomeDoBanco;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

Exemplo Camada API:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=seuServer;Database=nomeDoBanco;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Jwt": {
    "SecretKey": "sua#chave$secreta@aqui*2023",
    "Issuer": "user.net",
    "Audience": "http://www.user.net"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

3. No Package Manager Console, execute o comando `Update-Database` para aplicar as migrações e criar o banco de dados.

4. Pressione F5 para compilar e executar o projeto.

## Configurando o Projeto no Visual Studio Code

Para configurar e executar o projeto no Visual Studio Code, siga estas etapas:

1. Abra a pasta do projeto no Visual Studio Code.

2. Certifique-se de configurar a string de conexão com o seu banco de dados SQL Server no arquivo `appsettings.json`.

3. No Visual Studio Code, instale as seguintes extensões (se ainda não estiverem instaladas):
- [C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
- [NuGet Package Manager](https://marketplace.visualstudio.com/items?itemName=jmrog.vscode-nuget-package-manager)
- [Entity Framework Core Tools](https://marketplace.visualstudio.com/items?itemName=bengreenier.vscode-ef-core)
- [SQL Server (mssql)](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql)

4. Abra o terminal no Visual Studio Code e execute o comando `dotnet ef database update` para aplicar as migrações e criar o banco de dados.

5. Execute o projeto usando o comando `dotnet run` no terminal.

## Sobre o Projeto

Este projeto é um CRUD de produtos e categorias desenvolvido utilizando ASP.NET Core MVC, ASP.NET Core Web API, Entity Framework Core e SQL Server. 
Ele foi construído seguindo os princípios da Clean Architecture, que visa deixar o código bem definido, organizado e de fácil manutenção.

### Funcionalidades

- **CRUD de Produtos e Categorias:** O projeto permite a criação, leitura, atualização e exclusão de produtos e categorias.
- **Autenticação e Autorização:** Implementa autenticação e autorização via Identity, onde apenas o administrador tem acesso aos métodos de editar, deletar e criar.
- **Exportação de Dados para o Excel:** Inclui um método para exportar dados para o formato Excel.
- **API RESTful:** Além da interface web, o projeto também inclui uma camada de Web API com métodos RESTful para manipulação dos dados, utilizando autenticação JWT Bearer.
- **Consumindo Métodos da API na view** Também é possível consumir os métodos Get/GetById nas páginas web na camada de apresentação. Obs(**para executar essa função primeiro é necessário que você execute o projeto CleanArchMvc.API para executar a API local**).

### Clean Architecture

A arquitetura do projeto segue os princípios da Clean Architecture, que visa separar os componentes do sistema em camadas bem definidas, facilitando a manutenção e a evolução do software. As principais camadas incluem:

- **Domain:** Contém as entidades de domínio e as regras de negócio da aplicação.
- **Application:** Implementa os casos de uso da aplicação, servindo como uma camada intermediária entre a interface do usuário e o domínio.
- **Infrastructure:** Responsável pela implementação de detalhes técnicos, como acesso a banco de dados e integrações externas.
- **IoC:** Gerencia a injeção de dependência na aplicação.
- **Projeto de Apresentação:** Uma aplicação ASP.NET Core MVC que interage com as outras camadas do sistema.

### Curso Relacionado

Este projeto foi desenvolvido como parte de um curso sobre Clean Architecture e boas práticas de desenvolvimento em ASP.NET Core. 
O curso cobre desde os conceitos fundamentais da arquitetura limpa até a implementação de padrões de design como Repository e CQRS, além de abordar migrações para versões mais recentes do .NET.

Para mais informações sobre o curso e os conceitos abordados, consulte [Curso Base](https://www.udemy.com/share/104Fju3@EnFCbKISK5ThGDTadv8qy9VHhv2rCNA2Ej-ATEq7i6PAsmfgGiCPowcfyYM1Vq1IEA==/).

## Contribuindo

Contribuições são bem-vindas! Sinta-se à vontade para enviar pull requests ou abrir issues com feedback, sugestões ou correções.

## Status

Esse projeto ainda terá alterações futuras.
