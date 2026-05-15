# API E-commerce: Explorando ASP.NET Core e Arquiteturas Modernas

<img width="1983" height="793" alt="capa" src="https://github.com/user-attachments/assets/9e98ee56-70d9-4142-b28a-838bdb7c3f4a" />

## Visão Geral

Este projeto é uma **API RESTful** desenvolvida para simular um sistema de e-commerce, construída com **ASP.NET Core 8** e **C#**. Utilizo **Entity Framework Core** para a persistência de dados em **SQL Server**. Meu foco neste projeto foi explorar e aplicar conceitos de **modularidade**, **separação de responsabilidades** e **escalabilidade**, utilizando padrões de design que resultam em um código limpo e manutenível. É uma demonstração prática da minha proficiência em desenvolvimento backend com .NET.

## Objetivo do Projeto

O principal objetivo ao desenvolver esta API foi criar uma base robusta e extensível para operações de e-commerce. Através dela, busco gerenciar produtos, pedidos, carrinhos de compra e usuários, oferecendo endpoints bem definidos para consumo por aplicações frontend. Este projeto serve como um ambiente para aprofundar meus conhecimentos em ASP.NET Core e arquiteturas de software.

## Funcionalidades Implementadas

A API já oferece um conjunto de funcionalidades essenciais para um e-commerce:

*   **Gerenciamento de Produtos:** Operações CRUD completas para produtos, incluindo atributos como nome, preço e controle de estoque.
*   **Gerenciamento de Pedidos:** Criação, consulta e atualização de pedidos, com controle de status e itens associados.
*   **Carrinho de Compras:** Funcionalidades para adicionar, remover e atualizar itens no carrinho, com cálculo dinâmico de totais.
*   **Gerenciamento de Usuários:** Cadastro, consulta e manutenção de perfis de usuários.
*   **Processamento de Pagamentos:** Módulo preparado para simulação e controle de transações financeiras.

## Arquitetura e Design

A arquitetura do projeto foi pensada para ser flexível e escalável, combinando princípios de **Domain-Driven Design (DDD)** com uma organização modular por **Features (Vertical Slicing)**.

### Organização por Features

Adotei uma estrutura de pastas que agrupa todos os componentes relacionados a uma funcionalidade de negócio específica. Cada `Feature` (e.g., `Api.Pedidos`, `Api.Produtos`) encapsula seus próprios:

*   **Controllers:** Responsáveis por receber as requisições HTTP e orquestrar as chamadas de serviço.
*   **Services:** Contêm a lógica de negócio principal, garantindo o desacoplamento da camada de apresentação e persistência.
*   **Repositories:** Abstraem a lógica de acesso a dados, interagindo diretamente com o Entity Framework Core.
*   **Models:** Representam as entidades de domínio.
*   **DTOs (Data Transfer Objects):** Utilizados para entrada e saída de dados, mantendo a separação entre o modelo de domínio e o contrato da API.

Esta abordagem promove:

*   **Alta Coesão e Baixo Acoplamento:** Componentes de uma mesma funcionalidade estão logicamente agrupados, minimizando dependências entre features.
*   **Manutenibilidade Aprimorada:** Facilita a localização e modificação de código, reduzindo o risco de efeitos colaterais.
*   **Escalabilidade e Evolução:** Prepara o projeto para uma eventual transição para uma arquitetura de microserviços, onde cada feature poderia se tornar um serviço independente.

### Padrões de Projeto Aplicados

*   **Repository Pattern:** Abstrai a camada de persistência, permitindo que a lógica de negócio opere em coleções de objetos sem se preocupar com os detalhes de armazenamento.
*   **Service Layer:** Encapsula a lógica de negócio complexa, mantendo os `Controllers` enxutos e focados em HTTP.
*   **Dependency Injection (DI):** Utilizado extensivamente para gerenciar as dependências entre os componentes, promovendo flexibilidade e testabilidade.

### Tratamento de Erros Centralizado

Um `ErrorHandlingMiddleware` customizado garante que todas as exceções não tratadas sejam capturadas e transformadas em respostas HTTP padronizadas e amigáveis, com mensagens claras e códigos de status apropriados. Isso melhora a experiência do consumidor da API e a depuração.

### Logging Estruturado com Serilog

A aplicação utiliza **Serilog** para logging estruturado, configurado para registrar eventos em console e arquivos. Isso permite um monitoramento eficaz, facilitando a análise de logs e a identificação de problemas em ambientes de desenvolvimento e produção.

## Tecnologias Utilizadas

*   **Backend:** ASP.NET Core 8, C#
*   **ORM:** Entity Framework Core
*   **Banco de Dados:** SQL Server
*   **Logging:** Serilog
*   **Validação:** Data Annotations

## Estrutura de Pastas

```
ApiEcommerce/
├── src/
│   └── ApiEcommerce/
│       ├── Data/                  # Contexto do EF Core e configurações de banco de dados
│       ├── Extensions/            # Extensões para configuração de serviços (e.g., DI)
│       ├── Features/              # Módulos de funcionalidades (Vertical Slicing)
│       │   ├── Api.Carrinhos/
│       │   │   ├── Controllers/
│       │   │   ├── DTOs/          # Create, Response, Update DTOs
│       │   │   ├── Models/
│       │   │   ├── Repositories/
│       │   │   └── Services/
│       │   ├── Api.Pagamentos/
│       │   ├── Api.Pedidos/
│       │   ├── Api.Produtos/
│       │   └── Api.Usuarios/
│       ├── Migrations/            # Migrações do Entity Framework Core
│       ├── Properties/            # Configurações do projeto
│       ├── Shared/                # Componentes compartilhados (Middlewares, Exceptions, Enums)
│       ├── appsettings.json       # Configurações da aplicação
│       └── Program.cs             # Ponto de entrada da aplicação e configuração de pipeline
└── docs/                          # Documentação do projeto (e.g., Diagramas UML)
```

## Como Executar Localmente

Para configurar e executar este projeto em sua máquina local, siga os passos abaixo:

### Pré-requisitos

*   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
*   [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (ou SQL Server Express/LocalDB)
*   Um editor de código (e.g., Visual Studio, VS Code)

### Passos

1.  **Clone o Repositório:**
    ```bash
    git clone https://github.com/JohnVictor777/api-ecommerce.git
    cd api-ecommerce/src/ApiEcommerce
    ```

2.  **Configure a String de Conexão:**
    Abra o arquivo `appsettings.json` e atualize a `DefaultConnection` para apontar para sua instância do SQL Server. Exemplo:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=ApiEcommerce;Trusted_Connection=True;TrustServerCertificate=True;"
    }
    ```
    *Nota: Para produção, utilize variáveis de ambiente ou serviços de configuração seguros para a string de conexão.*

3.  **Aplique as Migrações do Banco de Dados:**
    Navegue até o diretório `src/ApiEcommerce` no terminal e execute os seguintes comandos:
    ```bash
    dotnet ef database update
    ```
    Isso criará o banco de dados e as tabelas necessárias.

4.  **Execute a Aplicação:**
    ```bash
    dotnet run
    ```
    A API estará disponível em `http://localhost:5088/swagger` (ou outra porta configurada).

## Exemplos de Endpoints

Você pode interagir com a API usando ferramentas como Postman, Insomnia ou cURL. Abaixo estão alguns exemplos de endpoints reais da aplicação:

### Produtos

*   **Listar todos os produtos:**
    `GET /api/Produto`

*   **Obter produto por ID:**
    `GET /api/Produto/{id}`

*   **Criar um novo produto:**
    `POST /api/Produto`
    ```json
    {
      "nome": "Smartphone X",
      "preco": 2500.00,
      "estoque": 10
    }
    ```

### Pedidos

*   **Listar todos os pedidos:**
    `GET /api/Pedido`

*   **Obter pedido por ID:**
    `GET /api/Pedido/{id}`

*   **Criar um novo pedido:**
    `POST /api/Pedido`
    ```json
    {
      "itens": [
        {
          "produtoId": "<GUID_DO_PRODUTO>",
          "quantidade": 1
        }
      ]
    }
    ```

### Carrinhos

*   **Listar todos os carrinhos:**
    `GET /api/Carrinho`

*   **Obter carrinho por ID:**
    `GET /api/Carrinho/{id}`

*   **Criar um novo carrinho:**
    `POST /api/Carrinho`
    ```json
    {
      "usuarioId": "<GUID_DO_USUARIO>",
      "itens": [
        {
          "produtoId": "<GUID_DO_PRODUTO>",
          "quantidade": 2
        }
      ]
    }
    ```

## Boas Práticas e Qualidade de Código

Este projeto demonstra a aplicação de diversas boas práticas de desenvolvimento:

*   **Princípios SOLID:** Aplicação de princípios como Single Responsibility Principle (SRP) e Open/Closed Principle (OCP) através da separação de camadas e modularidade.
*   **Código Limpo e Legível:** Nomenclatura clara, organização lógica e comentários quando necessário.
*   **Tratamento de Exceções:** Mecanismo centralizado para lidar com erros de forma consistente.
*   **Logging Robusto:** Utilização de Serilog para rastreabilidade e monitoramento.
*   **Validação de Entrada:** Garantia da integridade dos dados através de `Data Annotations` nos DTOs.
*   **Mapeamento ORM Eficiente:** Configuração de precisão para tipos decimais no Entity Framework Core, evitando problemas de arredondamento em valores monetários.

## Roadmap (Próximos Passos)

Para continuar aprimorando a robustez e a capacidade de produção desta API, os seguintes itens estão no meu roadmap:

*   **Autenticação e Autorização com JWT:** Implementar JSON Web Tokens para proteger os endpoints da API e controlar o acesso dos usuários com base em suas permissões.
*   **Testes Automatizados:** Desenvolver testes unitários e de integração para garantir a qualidade do código, prevenir regressões e facilitar futuras refatorações.
*   **Validação Avançada:** Adotar **FluentValidation** para regras de validação mais complexas e mensagens de erro personalizadas, proporcionando uma experiência de usuário mais rica e robusta.
*   **Padrão de Mensageria:** Integrar uma fila de mensagens (e.g., RabbitMQ, Kafka) para processamento assíncrono de operações que não precisam ser imediatas, como o processamento de pedidos, aumentando a resiliência e escalabilidade.
*   **Cache Distribuído:** Implementar cache com Redis para otimizar a performance de dados frequentemente acessados e que não mudam com muita frequência (e.g., lista de produtos, categorias).
*   **Containerização:** Adicionar `Dockerfile` e configurar a aplicação para execução em contêineres Docker, facilitando o deploy e a portabilidade em diferentes ambientes.
*   **CI/CD:** Configurar pipelines de Integração Contínua e Entrega Contínua (e.g., GitHub Actions) para automatizar o processo de build, teste e deploy, agilizando as entregas.
*   **Documentação Interativa:** Integrar e configurar o Swagger/OpenAPI para uma documentação interativa e atualizada da API, facilitando o consumo por desenvolvedores frontend.
*   **Segurança de Configuração:** Utilizar variáveis de ambiente ou serviços de segredos (e.g., Azure Key Vault) para gerenciar strings de conexão e outras informações sensíveis em produção, garantindo a segurança da aplicação.

## Diagrama de Arquitetura

Um diagrama UML inicial está disponível na pasta `docs/Diagrama-api-e-commerce.png`, fornecendo uma visão geral da estrutura e dos relacionamentos entre as entidades do sistema.

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull requests para melhorias e novas funcionalidades.

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).

---

**Desenvolvido por John Victor**

[GitHub](https://github.com/JohnVictor777) | [LinkedIn](https://www.linkedin.com/in/johnvic7or/)
