#PROJETO EM CONSTRUÇÃO (o outro repositório foi desativado)


# Game Store
O Game Store é um protótipo de uma loja de jogos construída utilizando ASP.NET 7, Docker, xUnit, Moq, Entity Framework, SQL Server, TDD, Clean Code, Princípios SOLID e Clean Archtecture.

## Pré-requisitos
Certifique-se de ter os seguintes pré-requisitos instalados em sua máquina:

- .NET SDK (versão 7 ou superior)
- Docker

## Primeiros Passos
Siga as etapas abaixo para começar com o projeto Game Store:

1. Clone o repositório:
    ```bash
    git clone https://github.com/chtwikee/game-store.git
    ```
    
2. Navegue até o diretório do projeto:
    ```bash
    cd game-store
    ```
    
3. Compile a solução:
    ```bash
    dotnet build
    ```
    
4. Execute a aplicação:
    ```bash
    dotnet run --project GameStore.API
    ```
    
5. Acesse a aplicação em seu navegador em http://localhost:5000.

## Executando os Testes
Para executar os testes, execute o seguinte comando no diretório do projeto:
    ```bash
    dotnet test
    ```
Isso irá executar todos os testes unitários utilizando o xUnit.


## Tecnologias Utilizadas
- Docker
- ASP.NET 7
- xUnit
- Moq
- Entity Framework
- SQL Server
- TDD (Test-Driven Development)
- Princípios Clean Code
- Princípios SOLID, DRY, YAGNI

### Estrutura de Pastas
A estrutura de pastas do projeto segue o padrão da Clean Archtecture:

- GameStore.API: Contém a camada da API, incluindo controladores e ponto de entrada da aplicação.
- GameStore.Application: Contém a camada de aplicação, incluindo casos de uso e lógica de negócio.
- GameStore.Domain: Contém a camada de domínio, incluindo entidades, objetos de valor e serviços de domínio.
- GameStore.Infrastructure: Contém a camada de infraestrutura, incluindo acesso a dados, repositórios e serviços externos.
- GameStore.API.Tests: Contém os testes unitários para as camadas de aplicação e domínio.

Sinta-se à vontade para personalizá-lo e utilizá-lo para construir sua própria loja de jogos.

## Reconhecimentos
### Este projeto foi inspirado nos princípios e práticas de desenvolvimento e arquitetura de software. Agradecimentos especiais à comunidade de código aberto por fornecer as ferramentas e frameworks utilizados neste projeto.
