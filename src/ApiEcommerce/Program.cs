using System.Data;
using System.Text;
using ApiEcommerce.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using ApiEcommerce.Services;
using Microsoft.IdentityModel.Tokens;
using ApiEcommerce;

var builder = WebApplication.CreateBuilder(args);

#region Configurar o Serilog para logging
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiEcommerce", Version = "v1" });

    // Define o esquema de segurança
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira: Bearer {seu_token}\n\nExemplo: Bearer eyJhbGci..."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
#endregion

#region Configurar a autenticação JWT

var jwtKey = builder.Configuration["Jwt:Key"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
        };
    });
#endregion

#region Adicionar serviços ao contêiner de injeção de dependência
// Adiciona o serviço de autorização e o TokenService para geração de tokens JWT
builder.Services.AddAuthorization();
builder.Services.AddScoped<TokenService>();

// Adiciona os serviços de controladores, Swagger e as dependências do projeto
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona as dependências do projeto e a infraestrutura, incluindo o DbContext e os repositórios
builder.Services.AddProjectDependencies();

// Configura a infraestrutura, incluindo o DbContext e os repositórios
builder.Services.AddInfrastructure(builder.Configuration);
#endregion

#region Configurar o Serilog para logging
// Configurações para o Serilog
var outputTemplate = "[{Timestamp:dd-MM-yyyy HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}";

var sinKOptions = new MSSqlServerSinkOptions
{
    TableName = "Logs",
    AutoCreateSqlTable = true,
};

var columnOptions = new ColumnOptions();
columnOptions.Store.Add(StandardColumn.LogEvent);
columnOptions.Store.Remove(StandardColumn.Properties);
columnOptions.LogEvent.DataLength = 4000;
columnOptions.Id.DataType = SqlDbType.BigInt;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(outputTemplate: outputTemplate)
    .WriteTo.File(
        "logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: outputTemplate
        )
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        sinkOptions: sinKOptions,
        columnOptions: columnOptions
    )
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();
#endregion

#region Configurar o middleware para usar HTTPS, autenticação, autorização e logging
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ApiEcommerce.Shared.Middlewares.RequestLoggingMiddleware>();
app.UseMiddleware<ApiEcommerce.Shared.Middlewares.ErrorHandlingMiddleware>();
app.MapControllers();
app.MapGet("/health", () => new { status = "OK" });

app.Run();
#endregion