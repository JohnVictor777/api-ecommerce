using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.Data;
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
            services.AddScoped<UsuarioRepository>();
            services.AddScoped<ProdutoRepository>();
            services.AddScoped<CarrinhoRepository>();
            services.AddScoped<PedidoRepository>();

            // Services
            services.AddScoped<UsuarioService>();
            services.AddScoped<ProdutoService>();
            services.AddScoped<PedidoService>();
            services.AddScoped<CarrinhoService>();

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ConnectionFactory>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}