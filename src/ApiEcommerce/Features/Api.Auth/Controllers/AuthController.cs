using ApiEcommerce.Features.Api.Auth.DTOs;
using ApiEcommerce.Features.Api.Auth.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiEcommerce.Features.Api.Auth.Controllers
{
    // Controlador de autenticação responsável por lidar com as requisições relacionadas à autenticação, como login e geração de tokens JWT.
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var token = await _authService.Login(dto);
            if (token == null)
                return Unauthorized(new { message = "E-mail ou senha inválidos." });

            return Ok(new { Token = token });
        }
    }
}
