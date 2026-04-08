# API E-commerce

## Visão Geral do Projeto

Este projeto é uma API RESTful desenvolvida em **ASP.NET Core 10.0** com **C#** e **Entity Framework Core**, projetada para simular as funcionalidades essenciais de um sistema de e-commerce. O objetivo principal é fornecer uma base para gerenciamento de usuários, produtos, carrinhos de compra e pedidos, servindo como um estudo prático de arquitetura de software, padrões de projeto e desenvolvimento de APIs.

## Tecnologias Utilizadas

*   **Backend:** ASP.NET Core 10.0, C#
*   **ORM:** Entity Framework Core 10.0
*   **Banco de Dados:** SQL Server (configurável via Connection String)
*   **Documentação da API:** Swagger/OpenAPI

## Funcionalidades Implementadas

Atualmente, a API oferece as seguintes funcionalidades básicas:

*   **Usuários:** Cadastro, listagem e exclusão de usuários.
*   **Produtos:** Cadastro, listagem e exclusão de produtos.
*   **Carrinhos de Compra:** Criação e listagem de carrinhos, permitindo adicionar itens.
*   **Pedidos:** Criação e listagem de pedidos, com cálculo do valor total e status.

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

## Como Rodar o Projeto

Para configurar e executar o projeto localmente, siga os passos abaixo:

1.  **Pré-requisitos:**
    *   .NET SDK 10.0 ou superior
    *   SQL Server (ou outro banco de dados compatível com Entity Framework Core)
    *   Um editor de código (ex: Visual Studio Code, Visual Studio)

2.  **Clonar o Repositório:**
    ```bash
    git clone https://github.com/JohnVictor777/api-ecommerce
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

    A API estará disponível em `http://localhost:5088` (ou outra porta configurada). Você pode acessar a documentação do Swagger em `http://localhost:5088/swagger`.

## Endpoints da API

*   **Carrinho**
    *   `GET /api/Carrinho`
    *   `POST /api/Carrinho`
    *   `DELETE /api/Carrinho/{id}`
*   **Pagamento**
    *   `GET /api/Pagamento`
*   **Pedido**
    *   `GET /api/Pedido`
    *   `POST /api/Pedido`
    *   `DELETE /api/Pedido/{id}`
*   **Produto**
    *   `GET /api/Produto`
    *   `POST /api/Produto`
    *   `DELETE /api/Produto/{id}`
*   **Usuário**
    *   `GET /api/Usuario`
    *   `POST /api/Usuario`
    *   `DELETE /api/Usuario/{id}`

## Esquemas (DTOs)

*   `CarrinhoCriarDTO`
*   `ItemCarrinhoCriarDTO`
*   `ItemPedidoCriarDTO`
*   `PedidoCriarDTO`
*   `ProdutoCriadoDTO`
*   `String<>f__AnonymousType9`
*   `UsuarioCreateDTO`

*(Consulte a documentação do Swagger para a lista completa de endpoints e seus contratos.)*

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

# E-commerce API

## Project Overview

This project is a RESTful API developed in **ASP.NET Core 10.0** with **C#** and **Entity Framework Core**, designed to simulate the essential functionalities of an e-commerce system. The main goal is to provide a foundation for user management, products, shopping carts, and orders, serving as a practical study of software architecture, design patterns, and API development.

## Technologies Used

*   **Backend:** ASP.NET Core 10.0, C#
*   **ORM:** Entity Framework Core 10.0
*   **Database:** SQL Server (configurable via Connection String)
*   **API Documentation:** Swagger/OpenAPI

## Implemented Features

Currently, the API offers the following basic functionalities:

*   **Users:** User registration, listing, and deletion.
*   **Products:** Product registration, listing, and deletion.
*   **Shopping Carts:** Creation and listing of shopping carts, allowing items to be added.
*   **Orders:** Creation and listing of orders, with total value calculation and status.

## Project Structure

The project follows a layered architecture, organized as follows:

*   `Controllers/`: Manages HTTP requests and API routing.
*   `Services/`: Contains the main business logic of the application.
*   `Repositories/`: Abstracts the data access layer, interacting with Entity Framework Core.
*   `Models/`: Defines the domain entities and system enums.
*   `DTOs/`: Data Transfer Objects for API input and output, ensuring separation between the domain model and API contracts.
*   `Data/`: Contains the `DbContext` (`ConnectionFactory`) for Entity Framework Core configuration.
*   `Middlewares/`: Includes custom middlewares, such as `ErrorHandlingMiddleware` for global exception handling.
*   `Migrations/`: Contains Entity Framework Core migrations for the database schema.

## How to Run the Project

To set up and run the project locally, follow the steps below:

1.  **Prerequisites:**
    *   .NET SDK 10.0 or higher
    *   SQL Server (or another database compatible with Entity Framework Core)
    *   A code editor (e.g., Visual Studio Code, Visual Studio)

2.  **Clone the Repository:**
    ```bash
    git clone https://github.com/JohnVictor777/api-ecommerce
    cd api-ecommerce/src/ApiEcommerce
    ```

3.  **Configure the Database:**
    *   Open the `appsettings.Development.json` (or `appsettings.json`) file and update the `ConnectionStrings:DefaultConnection` with your SQL Server credentials.
    *   Execute migrations to create the database and tables:
        ```bash
        dotnet ef database update
        ```

4.  **Run the Application:**
    ```bash
    dotnet run
    ```

    The API will be available at `http://localhost:5088` (or another configured port). You can access the Swagger documentation at `http://localhost:5088/swagger`.

## API Endpoints

*   **Cart**
    *   `GET /api/Carrinho`
    *   `POST /api/Carrinho`
    *   `DELETE /api/Carrinho/{id}`
*   **Payment**
    *   `GET /api/Pagamento`
*   **Order**
    *   `GET /api/Pedido`
    *   `POST /api/Pedido`
    *   `DELETE /api/Pedido/{id}`
*   **Product**
    *   `GET /api/Produto`
    *   `POST /api/Produto`
    *   `DELETE /api/Produto/{id}`
*   **User**
    *   `GET /api/Usuario`
    *   `POST /api/Usuario`
    *   `DELETE /api/Usuario/{id}`

## Schemas (DTOs)

*   `CarrinhoCriarDTO`
*   `ItemCarrinhoCriarDTO`
*   `ItemPedidoCriarDTO`
*   `PedidoCriarDTO`
*   `ProdutoCriadoDTO`
*   `String<>f__AnonymousType9`
*   `UsuarioCreateDTO`

*(Consult the Swagger documentation for the complete list of endpoints and their contracts.)*

## Areas for Improvement and Next Steps

This project is constantly evolving, and several areas have been identified for improvement and expansion. I am actively seeking feedback and collaboration from the community to enhance the following aspects:

1.  **Security:** Implementation of robust password hashing (e.g., BCrypt) and integration of JWT authentication to protect endpoints and manage user sessions.
2.  **Data Validation:** Addition of *Data Annotations* to input DTOs and implementation of more comprehensive validations to ensure the integrity of data received by the API.
3.  **Inventory Control:** Develop the logic to check product availability in stock before order creation and deduct sold quantities.
4.  **Payment Flow:** Full implementation of the payment module, including integration (even if simulated) with payment gateways and updating order status.
5.  **Error Handling:** Refactoring of the `ErrorHandlingMiddleware` to return more specific HTTP *status codes* (e.g., 400 Bad Request, 404 Not Found) and use the `ProblemDetails` standard for more informative and secure error responses.
6.  **Data Access Optimization:** Adjust repositories to include eager loading (`.Include()`) of related entities, especially for `Cart` and `Order`, to avoid *lazy loading* issues and ensure all necessary data is loaded.
7.  **Testing:** Implementation of unit and integration tests to ensure code robustness and quality.

## Contribution

Contributions are very welcome! If you have suggestions, find bugs, or want to implement any of the listed improvements, feel free to open an *issue* or submit a *pull request*.

## License

This project is licensed under the MIT License. See the `LICENSE` file for more details.

---
