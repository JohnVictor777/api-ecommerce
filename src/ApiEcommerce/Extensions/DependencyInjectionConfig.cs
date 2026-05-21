using ApiEcommerce.Data;

using ApiEcommerce.Features.Api.Usuarios.Repositories;
using ApiEcommerce.Features.Api.Usuarios.Services;

using ApiEcommerce.Features.Api.Produtos.Repositories;
using ApiEcommerce.Features.Api.Produtos.Services;

using ApiEcommerce.Features.Api.Carrinhos.Repositories;
using ApiEcommerce.Features.Api.Carrinhos.Services;

using ApiEcommerce.Features.Api.Pedidos.Repositories;
using ApiEcommerce.Features.Api.Pedidos.Services;

using ApiEcommerce.Features.Api.Pagamentos.Repositories;
using ApiEcommerce.Features.Api.Pagamentos.Services;

using ApiEcommerce.Repositories;
using ApiEcommerce.Services;

using Microsoft.EntityFrameworkCore;


namespace ApiEcommerce.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();

            // Services
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<ICarrinhoService, CarrinhoService>();
            services.AddScoped<IPagamentoService, PagamentoService>();

            return services;
        }

        // Infraestrutura
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ConnectionFactory>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}