# API E-commerce

## Visão Geral do Projeto

Este projeto é uma API RESTful desenvolvida em **ASP.NET Core 10.0** com **C#** e **Entity Framework Core**, projetada para simular as funcionalidades essenciais de um sistema de e-commerce. O objetivo principal é fornecer uma base para gerenciamento de usuários, produtos, carrinhos de compra e pedidos, servindo como um estudo prático de arquitetura de software, padrões de projeto e desenvolvimento de APIs.

---

## Tecnologias Utilizadas

*   **Backend:** ASP.NET Core 10.0, C#
*   **ORM:** Entity Framework Core 10.0
*   **Banco de Dados:** SQL Server (configurável via Connection String)
*   **Documentação da API:** Swagger/OpenAPI

---

## Funcionalidades Implementadas

Atualmente, a API oferece as seguintes funcionalidades básicas:

*   **Usuários:** Cadastro, listagem e exclusão de usuários.
*   **Produtos:** Cadastro, listagem e exclusão de produtos.
*   **Carrinhos de Compra:** Criação e listagem de carrinhos, permitindo adicionar itens.
*   **Pedidos:** Criação e listagem de pedidos, com cálculo do valor total e status.

---

## Estrutura do Projeto

O projeto segue uma arquitetura em camadas, organizada da seguinte forma:

*   `Controllers/`: Gerencia as requisições HTTP e roteamento da API.
*   `Services/`: Contém a lógica de negócio principal da aplicação.
*   `Repositories/`: Abstrai a camada de acesso a dados, interagindo com o Entity Framework Core.
*   `Models/`: Define as entidades de domínio e os enums do sistema.
*   `DTOs/`: Data Transfer Objects para entrada e saída de dados da API, garantindo a separação entre o modelo de domínio e os contratos da API.
*   `Data/`: Contém o `DbContext` (`ConnectionFactory`) para configuração do Entity Framework Core.
*   `Middlewares/`: Inclui middlewares customizados, como o `ErrorHandlingMiddleware` para tratamento global de exceções.
*   `Migrations/`: Contém as migrações do Entity Framework Core para o esquema do banco de dados.

---

## 🔗 Relacionamentos principais

* Um usuário possui um carrinho
* Um carrinho possui vários itens
* Cada item do carrinho está associado a um produto
* Um pedido possui vários itens
* Cada item do pedido referencia um produto
* Um pedido possui um pagamento

---

## Como Rodar o Projeto

Para configurar e executar o projeto localmente, siga os passos abaixo:

1.  **Pré-requisitos:**
    *   .NET SDK 10.0 ou superior
    *   SQL Server (ou outro banco de dados compatível com Entity Framework Core)
    *   Um editor de código (ex: Visual Studio Code, Visual Studio)

2.  **Clonar o Repositório:**
    ```bash
    git clone <URL_DO_SEU_REPOSITORIO>
    cd api-ecommerce/src/ApiEcommerce
    ```

3.  **Configurar o Banco de Dados:**
    *   Abra o arquivo `appsettings.Development.json` (ou `appsettings.json`) e atualize a `ConnectionStrings:DefaultConnection` com as credenciais do seu SQL Server.
    *   Execute as migrações para criar o banco de dados e as tabelas:
        ```bash
        dotnet ef database update
        ```

4.  **Executar a Aplicação:**
    ```bash
    dotnet run
    ```

    A API estará disponível em `https://localhost:7214` (ou outra porta configurada). Você pode acessar a documentação do Swagger em `https://localhost:7214/swagger`.
---

## Endpoints da API (Exemplos)

*   `GET /api/Usuario`: Lista todos os usuários.
*   `POST /api/Usuario`: Cria um novo usuário.
*   `GET /api/Produto`: Lista todos os produtos.
*   `POST /api/Produto`: Cria um novo produto.
*   `POST /api/Carrinho`: Cria um novo carrinho de compras.
*   `POST /api/Pedido`: Cria um novo pedido.

*(Consulte a documentação do Swagger para a lista completa de endpoints e seus contratos.)*

---

## Pontos de Melhoria e Próximos Passos

Este projeto está em constante evolução e há diversas áreas identificadas para melhoria e expansão. Estou ativamente buscando feedback e colaboração da comunidade para aprimorar os seguintes aspectos:

1.  **Segurança:** Implementação de *hashing* de senhas robusto (ex: BCrypt) e integração de autenticação JWT para proteger os endpoints e gerenciar sessões de usuário.
2.  **Validação de Dados:** Adição de *Data Annotations* nos DTOs de entrada e implementação de validações mais abrangentes para garantir a integridade dos dados recebidos pela API.
3.  **Controle de Estoque:** Desenvolver a lógica para verificar a disponibilidade de produtos no estoque antes da criação de pedidos e deduzir as quantidades vendidas.
4.  **Fluxo de Pagamento:** Implementação completa do módulo de pagamento, incluindo integração (mesmo que simulada) com gateways de pagamento e atualização do status do pedido.
5.  **Tratamento de Erros:** Refatoração do `ErrorHandlingMiddleware` para retornar *status codes* HTTP mais específicos (ex: 400 Bad Request, 404 Not Found) e utilizar o padrão `ProblemDetails` para respostas de erro mais informativas e seguras.
6.  **Otimização de Acesso a Dados:** Ajustar os repositórios para incluir o carregamento ansioso (`.Include()`) de entidades relacionadas, especialmente para `Carrinho` e `Pedido`, a fim de evitar problemas de *lazy loading* e garantir que todos os dados necessários sejam carregados.
7.  **Testes:** Implementação de testes unitários e de integração para garantir a robustez e a qualidade do código.

## Contribuição

Contribuições são muito bem-vindas! Se você tiver sugestões, encontrar bugs ou quiser implementar alguma das melhorias listadas, sinta-se à vontade para abrir uma *issue* ou enviar um *pull request*.

## Licença

Este projeto está licenciado sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

---
