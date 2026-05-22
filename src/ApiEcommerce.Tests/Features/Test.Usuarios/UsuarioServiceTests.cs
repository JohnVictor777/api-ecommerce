using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.DTOs;
using ApiEcommerce.Features.Api.Usuarios.DTOs.Update;
using ApiEcommerce.Features.Api.Usuarios.Repositories;
using ApiEcommerce.Features.Api.Usuarios.Services;
using ApiEcommerce.Models;
using ApiEcommerce.Services;
using ApiEcommerce.Shared.Exceptions;
using Moq;

namespace ApiEcommerce.Tests.Features.Usuarios
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _repoMock;


        private readonly IUsuarioService _service;

        public UsuarioServiceTests()
        {
            _repoMock = new Mock<IUsuarioRepository>();
            _service = new UsuarioService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAll_DeveRetornarListaDeUsuarios()
        {
            // Arrange
            var usuarios = new List<Usuario>
            {
                new() { Id = Guid.NewGuid(), Nome = "John Doe", Email = "john.doe@example.com" },
                new() { Id = Guid.NewGuid(), Nome = "Jane Smith", Email = "jane.smith@example.com" }
            };

            _repoMock.Setup(r => r.GetAll()).ReturnsAsync(usuarios);

            // Act
            var resultado = await _service.GetAll();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count);
            Assert.Contains(resultado, p => p.Nome == "John Doe");
        }

        [Fact]
        public async Task Create_DeveGerarNovoId()
        {
            // Arrange
            Usuario? usuarioCriado = null;

            _repoMock.Setup(r => r.Add(It.IsAny<Usuario>()))

                     .Callback<Usuario>(u => usuarioCriado = u)
                     .Returns(Task.CompletedTask);

            var dto = new UsuarioCreateDTO { Nome = "John Doe", Email = "john.doe@example.com" };

            // Act
            await _service.Create(dto);

            // Assert
            Assert.NotNull(usuarioCriado);
            Assert.NotEqual(Guid.Empty, usuarioCriado.Id);
        }

        [Fact]
        public async Task Update_UsuarioExistente_DeveAtualizarNomeEEmail()
        {
            // Arrange
            var id = Guid.NewGuid();
            var usuario = new Usuario { Id = id, Nome = "John Doe", Email = "john.doe@example.com" };

            _repoMock.Setup(r => r.GetById(id)).ReturnsAsync(usuario);
            _repoMock.Setup(r => r.Update(It.IsAny<Usuario>())).Returns(Task.CompletedTask);

            var dto = new UsuarioUpdateDTO { Nome = "  Novo Nome  ", Email = "new.email@example.com" };

            // Act
            await _service.Update(id, dto);

            // Assert
            Assert.Equal("Novo Nome", usuario.Nome);
            Assert.Equal("new.email@example.com", usuario.Email);

            _repoMock.Verify(r => r.Update(usuario), Times.Once);
        }

        [Fact]
        public async Task Delete_UsuarioInexistente_DeveLancarNotFoundException()
        {
            // Arrange 
            _repoMock.Setup(r => r.GetById(It.IsAny<Guid>()))
                     .ReturnsAsync((Usuario?)null);

            // Act
            await Assert.ThrowsAsync<NotFoundException>(
                () => _service.Delete(Guid.NewGuid())
            );
        }
    }
}