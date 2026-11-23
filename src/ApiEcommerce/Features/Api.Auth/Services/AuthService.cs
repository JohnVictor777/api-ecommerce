using ApiEcommerce.Features.Api.Auth.DTOs;
using ApiEcommerce.Features.Api.Usuarios.Repositories;

namespace ApiEcommerce.Features.Api.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly TokenService _tokenService;

        public AuthService(IUsuarioRepository usuarioRepository, TokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        public async Task<string?> Login(LoginDTO dto)
        {
            var usuario = await _usuarioRepository.GetByEmail(dto.Email.Trim());

            if (usuario == null || !SenhaConfere(dto.Senha, usuario.Senha))
                return null;

            return _tokenService.GerarToken(usuario.Id, usuario.Email, usuario.Role);
        }

        private static bool SenhaConfere(string senha, string hash)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(senha, hash);
            }
            catch (BCrypt.Net.SaltParseException)
            {
                // Senha gravada antes da adoção do hash: nunca é aceita.
                return false;
            }
        }
    }
}
