# API E-commerce: Explorando ASP.NET Core e Arquiteturas Modernas

<img width="1983" height="793" alt="capa" src="https://github.com/user-attachments/assets/9e98ee56-70d9-4142-b28a-838bdb7c3f4a" />

## Visão Geral

Este projeto é uma **API RESTful** desenvolvida para simular um sistema de e-commerce, construída com **ASP.NET Core 10** e **C#**. Utilizo **Entity Framework Core** para a persistência de dados em **SQL Server**. Meu foco neste projeto foi explorar e aplicar conceitos de **modularidade**, **separação de responsabilidades** e **escalabilidade**, utilizando padrões de design que resultam em um código limpo e manutenível. É uma demonstração prática da minha proficiência em desenvolvimento backend com .NET.

## Objetivo do Projeto

O principal objetivo ao desenvolver esta API foi criar uma base robusta e extensível para operações de e-commerce. Através dela, busco gerenciar produtos, pedidos, carrinhos de compra e usuários, oferecendo endpoints bem definidos para consumo por aplicações frontend. Este projeto serve como um ambiente para aprofundar meus conhecimentos em ASP.NET Core e arquiteturas de software.

## Funcionalidades Implementadas

A API já oferece um conjunto de funcionalidades essenciais para um e-commerce:

*   **Autenticação e Autorização JWT:** Proteção de endpoints sensíveis utilizando JSON Web Tokens, com suporte a perfis de acesso (Roles).
*   **Gerenciamento de Produtos:** Operações CRUD completas para produtos, incluindo atributos como nome, preço e controle de estoque.
*   **Gerenciamento de Pedidos:** Criação, consulta e atualização de pedidos, com controle de status e itens associados.
*   **Carrinho de Compras:** Funcionalidades para adicionar, remover e atualizar itens no carrinho, com cálculo dinâmico de totais.
*   **Gerenciamento de Usuários:** Cadastro, consulta e manutenção de perfis de usuários com hash de senha seguro (BCrypt).
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

### Padrões de Projeto Aplicados

*   **Repository Pattern:** Abstrai a camada de persistência, permitindo que a lógica de negócio opere em coleções de objetos sem se preocupar com os detalhes de armazenamento.
*   **Service Layer:** Encapsula a lógica de negócio complexa, mantendo os `Controllers` enxutos e focados em HTTP.
*   **Dependency Injection (DI):** Utilizado extensivamente para gerenciar as dependências entre os componentes, promovendo flexibilidade e testabilidade.
*   **JWT Authentication:** Implementação de segurança stateless para escalabilidade.

### Tratamento de Erros Centralizado

Um `ErrorHandlingMiddleware` customizado garante que todas as exceções não tratadas sejam capturadas e transformadas em respostas HTTP padronizadas e amigáveis, com mensagens claras e códigos de status apropriados.

### Logging Estruturado com Serilog

A aplicação utiliza **Serilog** para logging estruturado, configurado para registrar eventos em console, arquivos e **SQL Server**. Isso permite um monitoramento eficaz e facilita a identificação de problemas em produção.

## Tecnologias Utilizadas

*   **Backend:** ASP.NET Core 10, C#
*   **Segurança:** JWT (JSON Web Token), BCrypt.Net
*   **ORM:** Entity Framework Core
*   **Banco de Dados:** SQL Server
*   **Logging:** Serilog (Console, File, MSSqlServer)
*   **Documentação:** Swagger/OpenAPI com suporte a Auth Bearer

## Estrutura de Pastas

```
ApiEcommerce/
├── src/
│   └── ApiEcommerce/
│       ├── Data/                  # Contexto do EF Core e configurações de banco de dados
│       ├── Extensions/            # Extensões para configuração de serviços (e.g., DI)
│       ├── Features/              # Módulos de funcionalidades (Vertical Slicing)
│       │   ├── Api.Auth/          # Gerenciamento de Login e Tokens JWT
│       │   ├── Api.Carrinhos/
│       │   ├── Api.Pagamentos/
│       │   ├── Api.Pedidos/
│       │   ├── Api.Produtos/
│       │   └── Api.Usuarios/
│       ├── Migrations/            # Migrações do Entity Framework Core
│       ├── Properties/            # Configurações do projeto
│       ├── Shared/                # Componentes compartilhados (Middlewares, Exceptions, Services)
│       │   ├── Middlewares/
│       │   └── Services/          # TokenService e outros serviços globais
│       ├── appsettings.json       # Configurações da aplicação
│       └── Program.cs             # Ponto de entrada da aplicação e configuração de pipeline
└── docs/                          # Documentação do projeto (e.g., Diagramas UML)
```

## Como Executar Localmente

### Pré-requisitos

*   [.NET 10 SDK](https://dotnet.microsoft.com/download)
*   [SQL Server]
*   Um editor de código (e.g., Visual Studio, VS Code)

### Passos

1.  **Clone o Repositório:**
    ```bash
    git clone https://github.com/JohnVictor777/api-ecommerce.git
    cd api-ecommerce/src/ApiEcommerce
    ```

2.  **Configuração de Segurança (JWT):**
    Gere uma chave e configure o user-secrets:
    ```bash
    dotnet user-secrets init
    dotnet user-secrets set "Jwt:Key" "SUA_CHAVE_SECRETA_32_CHARS"
    dotnet user-secrets set "Jwt:Issuer" "ApiEcommerce"
    dotnet user-secrets set "Jwt:Audience" "EcommerceClient"
    dotnet user-secrets set "Jwt:ExpireMinutes" "60"
    ```

3.  **Configure o Banco de Dados:**
    Atualize a `DefaultConnection` no `appsettings.json` e execute:
    ```bash
    dotnet ef database update
    ```

4.  **Execute a Aplicação:**
    ```bash
    dotnet run
    ```
    Acesse `http://localhost:5088/swagger` para testar.

## Segurança e Autenticação

Para acessar endpoints protegidos:
1.  Faça login via `POST /api/Auth/login`.
2.  Copie o Token JWT retornado.
3.  No Swagger, clique em **Authorize** e insira: `Bearer SEU_TOKEN`.

## Roadmap (Próximos Passos)

*   [x] **Autenticação e Autorização com JWT**
*   [ ] **Testes Automatizados** (Unitários e Integração)
*   [ ] **Validação Avançada** com FluentValidation
*   [ ] **Padrão de Mensageria** (RabbitMQ/Kafka)
*   [ ] **Cache Distribuído** com Redis
*   [ ] **Containerização** com Docker
*   [ ] **CI/CD** com GitHub Actions

---

**Desenvolvido por John Victor**

[GitHub](https://github.com/JohnVictor777) | [LinkedIn](https://www.linkedin.com/in/johnvic7or/)
