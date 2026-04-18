using System.Data;
using ApiEcommerce.Data;
using ApiEcommerce.Extensions;
using ApiEcommerce.Repositories;
using ApiEcommerce.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

#region Adicionar serviços ao contêiner de injeção de dependência
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar as dependências do projeto
builder.Services.AddProjectDependencies();

// Configurar o DbContext para usar SQL Server
builder.Services.AddInfrastructure(builder.Configuration);
#endregion

#region Configurar o Serilog para logging
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


var app = builder.Build();

#region Configurar o middleware para usar Swagger apenas em desenvolvimento

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endregion

app.UseHttpsRedirection();
app.UseMiddleware<ApiEcommerce.Shared.Middlewares.RequestLoggingMiddleware>();
app.UseMiddleware<ApiEcommerce.Middlewares.ErrorHandlingMiddleware>();
app.MapControllers();
app.MapGet("/health", () => new { status = "OK" });

app.Run();
