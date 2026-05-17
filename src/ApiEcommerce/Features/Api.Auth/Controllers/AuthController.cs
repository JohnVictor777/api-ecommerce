using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiEcommerce.Features.Api.Auth.Controllers
{
    // Controlador de autenticação responsável por lidar com as requisições relacionadas à autenticação, como login e geração de tokens JWT.
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        public AuthController(TokenService tokenService) => _tokenService = tokenService;

        [HttpPost("login")]
        public IActionResult Login()
        {
            var token = _tokenService.GerarToken(Guid.NewGuid(), "teste@teste.com", "Admin");
            return Ok(new { Token = token });
        }
    }
}