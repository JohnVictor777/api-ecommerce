using Serilog;
using ApiEcommerce.Features.Api.Usuarios.Repositories;
using ApiEcommerce.Models;
using ApiEcommerce.Shared;

namespace ApiEcommerce.Data
{
    public static class AdminSeeder
    {
        // Cria o usuário administrador definido em Admin:Email / Admin:Senha, caso ainda não exista.
        public static async Task SeedAdminAsync(this IServiceProvider services, IConfiguration configuration)
        {
            var email = configuration["Admin:Email"]?.Trim();
            var senha = configuration["Admin:Senha"];

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
                return;

            using var scope = services.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IUsuarioRepository>();

            var existente = await repository.GetByEmail(email);
            if (existente != null)
            {
                if (existente.Role != Roles.Admin)
                    Log.Warning("Administrador não criado: o e-mail configurado em Admin:Email já pertence a um usuário comum.");
                return;
            }

            await repository.Add(new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = "Administrador",
                Email = email,
                Senha = BCrypt.Net.BCrypt.HashPassword(senha),
                Role = Roles.Admin
            });
        }
    }
}
