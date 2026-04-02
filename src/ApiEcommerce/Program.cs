using ApiEcommerce.Data;
using ApiEcommerce.Repositories;
using ApiEcommerce.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner de injeção de dependência
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UsuarioRepositorie>();
builder.Services.AddScoped<ProdutoRepositorie>();
builder.Services.AddScoped<PedidoRepository>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<PedidoService>();

// Configurar o DbContext para usar SQL Server
builder.Services.AddDbContext<ConnectionFactory>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configurar o middleware para usar Swagger apenas em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ApiEcommerce.Middlewares.ErrorHandlingMiddleware>();
app.MapControllers();
app.MapGet("/health", () => new { status = "OK" });

app.Run();
