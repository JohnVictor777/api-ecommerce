# Estágio de construção (Build)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copiar arquivos de projeto e restaurar dependências
COPY src/ApiEcommerce/ApiEcommerce.csproj ./src/ApiEcommerce/
RUN dotnet restore src/ApiEcommerce/ApiEcommerce.csproj

# Copiar o restante do código e publicar
COPY . .
WORKDIR /app/src/ApiEcommerce
RUN dotnet publish -c Release -o /app/publish

# Estágio final (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expor as portas da aplicação
EXPOSE 8080
EXPOSE 8081

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "ApiEcommerce.dll"]
