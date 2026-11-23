using ApiEcommerce.Features.Api.Auth.DTOs;

namespace ApiEcommerce.Features.Api.Auth.Services
{
    public interface IAuthService
    {
        // Retorna o token JWT, ou null quando as credenciais são inválidas.
        Task<string?> Login(LoginDTO dto);
    }
}
