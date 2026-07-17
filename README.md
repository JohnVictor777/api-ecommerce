# API E-commerce: Explorando ASP.NET Core e Arquiteturas Modernas

<img width="1983" height="793" alt="capa" src="https://github.com/user-attachments/assets/9e98ee56-70d9-4142-b28a-838bdb7c3f4a" />

## Visão Geral

Este projeto é uma API RESTful desenvolvida para simular um sistema de e-commerce, construída com **ASP.NET Core** e **C#**. Utilizo **Entity Framework Core** para a persistência de dados em **SQL Server**. Meu foco neste projeto foi explorar e aplicar conceitos de modularidade, separação de responsabilidades e escalabilidade, utilizando padrões de design que resultam em um código limpo e manutenível. É uma demonstração prática da minha proficiência em desenvolvimento backend com .NET.

## Objetivo do Projeto

O principal objetivo ao desenvolver esta API foi criar uma base robusta e extensível para operações de e-commerce. Através dela, busco gerenciar produtos, pedidos, carrinhos de compra e usuários, oferecendo endpoints bem definidos para consumo por aplicações frontend. Este projeto serve como um ambiente para aprofundar meus conhecimentos em ASP.NET Core e arquiteturas de software.

---

## Funcionalidades Implementadas

A API já oferece um conjunto de funcionalidades essenciais para um e-commerce:

- **Autenticação e Autorização JWT:** Proteção de endpoints sensíveis utilizando JSON Web Tokens, com suporte a perfis de acesso (Roles). *(veja a nota sobre o estado atual do login na seção "Segurança e Autenticação")*

- **Gerenciamento de Produtos:** Operações CRUD completas para produtos, incluindo atributos como nome, preço e controle de estoque.

- **Gerenciamento de Pedidos:** Criação, consulta e atualização de pedidos, com controle de status e itens associados.

- **Carrinho de Compras:** Funcionalidades para adicionar, remover e atualizar itens no carrinho, com cálculo dinâmico de totais.

- **Gerenciamento de Usuários:** Cadastro, consulta e manutenção de perfis de usuários com hash de senha seguro (BCrypt).

- **Processamento de Pagamentos:** Módulo preparado para simulação e controle de transações financeiras.

- **Testes Unitários:** Estrutura inicial criada e organizada por Feature para validação automatizada das regras de negócio, utilizando xUnit e Moq.

---

## Arquitetura e Design

A arquitetura do projeto foi pensada para ser flexível e escalável, combinando princípios de **Domain-Driven Design (DDD)** com uma organização modular por **Features (Vertical Slicing)**.

### Organização por Features

Adotei uma estrutura de pastas que agrupa todos os componentes relacionados a uma funcionalidade de negócio específica. Cada Feature (e.g., `Api.Pedidos`, `Api.Produtos`) encapsula seus próprios:

- **Controllers:** Responsáveis por receber as requisições HTTP e orquestrar as chamadas de serviço.
- **Services:** Contêm a lógica de negócio principal, garantindo o desacoplamento da camada de apresentação e persistência.
- **Repositories:** Abstraem a lógica de acesso a dados, interagindo diretamente com o Entity Framework Core.
- **Models:** Representam as entidades de domínio.
- **DTOs (Data Transfer Objects):** Utilizados para entrada e saída de dados, mantendo a separação entre o modelo de domínio e o contrato da API.

### Padrões de Projeto Aplicados

- **Repository Pattern:** Abstrai a camada de persistência, permitindo que a lógica de negócio opere em coleções de objetos sem se preocupar com os detalhes de armazenamento.
- **Service Layer:** Encapsula a lógica de negócio complexa, mantendo os Controllers enxutos e focados em HTTP.
- **Dependency Injection (DI):** Utilizado extensivamente para gerenciar as dependências entre os componentes, promovendo flexibilidade e testabilidade.
- **JWT Authentication:** Implementação de segurança stateless para escalabilidade.

### Exceções de Domínio

O projeto utiliza exceções próprias (`NotFoundException`, `ValidationException`) para representar erros de negócio de forma semântica, separadas das exceções de infraestrutura.

### Middlewares Globais

Dois middlewares no pipeline centralizam comportamentos transversais:

- **`ErrorHandlingMiddleware`** — captura todas as exceções não tratadas e retorna respostas HTTP padronizadas com mensagens claras e códigos de status apropriados.
- **`RequestLoggingMiddleware`** — registra todas as requisições recebidas, incluindo método, rota e tempo de resposta, integrado ao Serilog.

### Injeção de Dependência Centralizada

Toda a configuração de serviços é feita em `Extensions/DependencyInjectionConfig.cs`, mantendo o `Program.cs` limpo e o registro de dependências organizado por domínio.

### Logging Estruturado com Serilog

A aplicação utiliza **Serilog** para logging estruturado, configurado para registrar eventos em console, arquivos e SQL Server. Isso permite um monitoramento eficaz e facilita a identificação de problemas em produção.

### Configuração via Variáveis de Ambiente

O projeto usa `dotenv.net` para carregar um arquivo `.env` local (não versionado) somado a `AddEnvironmentVariables()`. Isso vale tanto para rodar via `dotnet run` direto no host quanto para o `docker-compose.yml` — os dois cenários lêem as mesmas chaves de configuração (`ConnectionStrings__DefaultConnection`, `Jwt__Key`, etc.), só que de arquivos `.env` diferentes (veja a seção "Como Executar Localmente").

---

## Testes Automatizados

O projeto possui estrutura dedicada de testes unitários organizada por domínio, utilizando:

| Ferramenta | Papel |
|---|---|
| **xUnit** | Framework de testes |
| **Moq** | Mocking de dependências |
| **Microsoft.NET.Test.Sdk** | Suporte à execução dos testes |

Features com estrutura de testes criada:

✔ Auth  
✔ Carrinhos  
✔ Pagamentos  
✔ Pedidos  
✔ Produtos  
✔ Usuários  

Executar todos os testes:

```bash
dotnet test
```

> A organização por Feature (`Test.Auth`, `Test.Pedidos`, etc.) facilita manutenção, escalabilidade e rastreabilidade das regras de negócio testadas.

---

## Tecnologias Utilizadas

**Backend:**
- C#
- ASP.NET Core
- Entity Framework Core

**Banco de Dados:**
- SQL Server (containerizado via Docker)

**Segurança:**
- JWT (JSON Web Token)
- BCrypt.Net

**Configuração:**
- dotenv.net (variáveis de ambiente via `.env`)

**Documentação:**
- Swagger/OpenAPI com suporte a Auth Bearer

**Testes:**
- xUnit
- Moq
- Microsoft.NET.Test.Sdk

**Logs:**
- Serilog (Console, File, MSSqlServer)

---

## Estrutura de Pastas

```txt
api-ecommerce/
│
├── docs/
│
└── src/
    │
    ├── ApiEcommerce/
    │   │
    │   ├── Data/                        # DbContext e configurações do EF Core
    │   │
    │   ├── Extensions/
    │   │   └── DependencyInjectionConfig.cs  # Registro centralizado de todos os serviços
    │   │
    │   ├── Features/                    # Módulos por domínio (Vertical Slicing)
    │   │   ├── Api.Auth/
    │   │   ├── Api.Carrinhos/
    │   │   ├── Api.Pagamentos/
    │   │   ├── Api.Pedidos/
    │   │   ├── Api.Produtos/
    │   │   └── Api.Usuarios/
    │   │
    │   ├── Migrations/                  # Migrações geradas pelo EF Core
    │   │
    │   ├── Shared/                      # Componentes transversais reutilizáveis
    │   │   ├── Exceptions/
    │   │   │   ├── NotFoundException.cs
    │   │   │   └── ValidationException.cs
    │   │   ├── Middlewares/
    │   │   │   ├── ErrorHandlingMiddleware.cs
    │   │   │   └── RequestLoggingMiddleware.cs
    │   │   ├── Services/
    │   │   │   └── TokenService.cs
    │   │   └── Enum.cs
    │   │
    │   ├── appsettings.json
    │   └── Program.cs
    │
    └── ApiEcommerce.Tests/
        └── Features/
            ├── Test.Auth/
            ├── Test.Carrinhos/
            ├── Test.Pagamentos/
            ├── Test.Pedidos/
            ├── Test.Produtos/
            └── Test.Usuarios/
```

### Anatomia Interna de cada Feature

Todas as Features seguem o mesmo padrão estrutural. Exemplo real com `Api.Carrinhos`:

```txt
Api.Carrinhos/
├── Controllers/
├── DTOs/
│   ├── Create/          # Payloads de entrada para criação
│   ├── Response/        # Modelos de resposta da API
│   └── Update/          # Payloads de entrada para atualização
├── Models/
├── Repositories/
│   ├── CarrinhoRepository.cs
│   └── ICarrinhoRepository.cs   # Interface co-localizada com a implementação
└── Services/
    ├── CarrinhoService.cs
    └── ICarrinhoService.cs      # Interface co-localizada com a implementação
```

> Cada Feature é autossuficiente e agrupa todos os seus artefatos. DTOs são segmentados por operação (Create / Response / Update). Interfaces vivem junto de suas implementações. Nenhuma Feature depende da estrutura interna de outra.

---

## Como Executar Localmente

**Pré-requisitos**
- .NET SDK
- SQL Server (local ou via Docker — veja a seção "Como rodar com Docker")
- Um editor de código (Visual Studio, VS Code, Rider)

**Passos**

Clone o Repositório:

```bash
git clone https://github.com/JohnVictor777/api-ecommerce.git
cd api-ecommerce/src/ApiEcommerce
```

Configuração de variáveis de ambiente — crie um arquivo `.env` **dentro de `src/ApiEcommerce/`** (não versionado, já ignorado pelo `.gitignore`):

```
Jwt__Key="uma-chave-aleatoria-com-pelo-menos-32-caracteres"
Jwt__Issuer="ApiEcommerce"
Jwt__Audience="ApiEcommerce"
Jwt__ExpireMinutes=60
ConnectionStrings__DefaultConnection="Server=localhost;Database=ApiEcommerce;User Id=sa;Password=SUA_SENHA_AQUI;TrustServerCertificate=True;"
ASPNETCORE_ENVIRONMENT=Development
```

> ⚠️ **Importante:** a connection string precisa usar autenticação SQL (`User Id=sa;Password=...`), não `Trusted_Connection=True` (autenticação integrada do Windows). O container de SQL Server rodando em Linux via Docker só aceita autenticação SQL — `Trusted_Connection=True` não funciona nesse cenário e vai gerar erro de conexão.
>
> Pra gerar uma chave JWT segura, use: `openssl rand -base64 32`

Configure o Banco de Dados:

```bash
dotnet ef database update
```

Se o comando não for reconhecido, instale a ferramenta global primeiro:
```bash
dotnet tool install --global dotnet-ef
```

Execute a Aplicação:

```bash
dotnet run
```

Acesse `http://localhost:5088/swagger` para testar.

**Executar os Testes:**

```bash
dotnet test
```

---

## Segurança e Autenticação

Para acessar endpoints protegidos:

1. Faça login via `POST /api/Auth/login`
2. Copie o Token JWT retornado
3. No Swagger, clique em **Authorize** e insira: `Bearer SEU_TOKEN`

> 🚧 **Estado atual do login:** o endpoint `/api/Auth/login` ainda está em desenvolvimento — atualmente ele gera um token JWT válido sem validar usuário ou senha reais. Ele existe para permitir testar o restante do pipeline de autenticação (geração de token, validação de assinatura, proteção de rotas com `[Authorize]`) enquanto a lógica de autenticação real (verificação de credenciais contra o banco, hash de senha) ainda não foi conectada a esse endpoint.

---

## Roadmap (Próximos Passos)

### Infraestrutura
- [x] JWT Authentication (geração e validação de token)
- [x] Serilog (Console, File, SQL Server)
- [x] Testes unitários — estrutura criada e organizada por Feature
- [x] Docker — containerização da API e do banco de dados
- [ ] Login real — validar usuário e senha contra o banco de dados
- [ ] Deploy — publicação em ambiente de produção (Azure / Railway / Render)
- [ ] GitHub Actions — CI/CD automatizado
- [ ] Validação Avançada com FluentValidation
- [ ] Redis — cache distribuído

### Arquitetura
- [ ] CQRS
- [ ] Mensageria com RabbitMQ/Kafka

---

## 🐳 Como rodar com Docker

Para facilitar o desenvolvimento e a execução da API, este projeto conta com suporte ao Docker.

### Pré-requisitos
- [Docker](https://www.docker.com/get-started)
- Docker Compose V2 (plugin `docker compose`, incluso nas instalações recentes do Docker Engine — **não** confundir com o binário legado `docker-compose`, que está descontinuado e pode falhar em sistemas com Python 3.12+)

### Passo a Passo

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/JohnVictor777/api-ecommerce.git
   cd api-ecommerce
   ```

2. **Configure as variáveis de ambiente:** crie um arquivo `.env` na raiz do repositório (mesma pasta do `docker-compose.yml`):
   ```
   MSSQL_SA_PASSWORD=EscolhaUmaSenhaForte!2026
   JWT_KEY=uma-chave-aleatoria-com-pelo-menos-32-caracteres
   ```

3. **Suba os containers:**
   ```bash
   docker compose up -d
   ```
   Este comando irá baixar as imagens necessárias, configurar o SQL Server e subir a API.

4. **Acesse a API:**
   A API estará disponível em `http://localhost:5000`.
   Você pode acessar o Swagger em `http://localhost:5000/swagger/index.html`.

5. **Rodar as migrations dentro do ambiente Docker:** se for a primeira vez subindo o banco, aplique as migrations apontando pro container (`Server=localhost,1433` a partir do host, já que a porta 1433 está mapeada).

---

## Desenvolvido por

**John Victor do E. Santo**

[![GitHub](https://img.shields.io/badge/GitHub-JohnVictor777-181717?logo=github)](https://github.com/JohnVictor777)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-johnvic7or-0A66C2?logo=linkedin)](https://linkedin.com/in/johnvic7or)