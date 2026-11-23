using ApiEcommerce.Features.Api.Auth.DTOs;
using ApiEcommerce.Features.Api.Auth.Services;
using ApiEcommerce.Features.Api.Usuarios.Repositories;
using ApiEcommerce.Models;
using ApiEcommerce.Shared;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiEcommerce.Tests.Auth
{
    public class AuthServiceTests
    {
        private readonly Mock<IUsuarioRepository> _repoMock;
        private readonly AuthService _service;
        private readonly Usuario _usuario;

        public AuthServiceTests()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { "Jwt:Key",           "chave-super-secreta-para-testes-unitarios-123!" },
                    { "Jwt:Issuer",        "ApiEcommerce" },
                    { "Jwt:Audience",      "ApiEcommerce" },
                    { "Jwt:ExpireMinutes", "60" }
                })
                .Build();

            _usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = "Admin",
                Email = "admin@teste.com",
                Senha = BCrypt.Net.BCrypt.HashPassword("senha-correta"),
                Role = Roles.Admin
            };

            _repoMock = new Mock<IUsuarioRepository>();
            _repoMock.Setup(r => r.GetByEmail(_usuario.Email)).ReturnsAsync(_usuario);

            _service = new AuthService(_repoMock.Object, new TokenService(config));
        }

        [Fact]
        public async Task Login_CredenciaisValidas_DeveRetornarTokenComIdERoleDoUsuario()
        {
            // Act
            var token = await _service.Login(new LoginDTO { Email = _usuario.Email, Senha = "senha-correta" });

            // Assert
            Assert.NotNull(token);
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            Assert.Equal(_usuario.Id.ToString(), jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value);
            Assert.Equal(Roles.Admin, jwt.Claims.First(c => c.Type == ClaimTypes.Role).Value);
        }

        [Fact]
        public async Task Login_SenhaIncorreta_DeveRetornarNull()
        {
            var token = await _service.Login(new LoginDTO { Email = _usuario.Email, Senha = "senha-errada" });

            Assert.Null(token);
        }

        [Fact]
        public async Task Login_EmailInexistente_DeveRetornarNull()
        {
            var token = await _service.Login(new LoginDTO { Email = "ninguem@teste.com", Senha = "senha-correta" });

            Assert.Null(token);
        }

        [Fact]
        public async Task Login_SenhaArmazenadaSemHash_DeveRetornarNull()
        {
            _usuario.Senha = "senha-correta";

            var token = await _service.Login(new LoginDTO { Email = _usuario.Email, Senha = "senha-correta" });

            Assert.Null(token);
        }
    }
}
