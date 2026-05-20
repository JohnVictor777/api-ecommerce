using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ApiEcommerce.Tests.Auth
{
    public class TokenServiceTests
    {
        private readonly TokenService _service;

        public TokenServiceTests()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {

                { "Jwt:Key",           "chave-super-secreta-para-testes-unitarios-123!" },
                { "Jwt:Issuer",        "ApiEcommerce"      },
                { "Jwt:Audience",      "ApiEcommerceUsers" },
                { "Jwt:ExpireMinutes", "60"                }
                })
                .Build();

            _service = new TokenService(config);
        }

        [Fact]
        public void GerarToken_DeveRetornarTokenNaoNulo()
        {
            // Arrange
            var id = Guid.NewGuid();
            var email = "teste@teste.com";
            var role = "Admin";

            // Act 
            var token = _service.GerarToken(id, email, role);

            // Assert 
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }

        [Fact]
        public void GerarToken_DeveRetornarFormatoJwtValido()
        {
            // Act
            var token = _service.GerarToken(Guid.NewGuid(), "teste@teste.com", "User");
            var partes = token.Split('.');

            // Assert
            Assert.Equal(3, partes.Length);
        }

        [Fact]
        public void GerarToken_DeveConterSubEmailERole()
        {
            // Arrange
            var id = Guid.NewGuid();
            var email = "joao@email.com";
            var role = "Admin";

            // Act
            var token = _service.GerarToken(id, email, role);
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var subClaim = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            var emailClaim = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email);
            var roleClaim = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            // Assert
            Assert.Equal(id.ToString(), subClaim?.Value);
            Assert.Equal(email, emailClaim?.Value);
            Assert.Equal(role, roleClaim?.Value);
        }

        [Fact]
        public void GerarToken_DeveExpirarEm60Minutos()
        {
            // Arrange
            var antes = DateTime.UtcNow;
            var token = _service.GerarToken(Guid.NewGuid(), "teste@teste.com", "User");
            var depois = DateTime.UtcNow;

            // Act
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

            // Assert
            Assert.True(jwt.ValidTo > antes.AddMinutes(59));
            Assert.True(jwt.ValidTo < depois.AddMinutes(61));
        }
    }
}