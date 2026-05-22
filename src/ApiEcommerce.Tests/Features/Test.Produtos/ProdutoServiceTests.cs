using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.DTOs;
using ApiEcommerce.Features.Api.Produtos.DTOs.Update;
using ApiEcommerce.Features.Api.Produtos.Repositories;
using ApiEcommerce.Features.Api.Produtos.Services;
using ApiEcommerce.Models;
using ApiEcommerce.Services;
using ApiEcommerce.Shared.Exceptions;
using Moq;

namespace ApiEcommerce.Tests.Features.Produtos
{
    public class ProdutoServiceTests
    {
        private readonly Mock<IProdutoRepository> _repoMock;


        private readonly IProdutoService _service;

        public ProdutoServiceTests()
        {
            _repoMock = new Mock<IProdutoRepository>();
            _service = new ProdutoService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAll_DeveRetornarListaDeProdutos()
        {
            // Arrange
            var produtos = new List<Produto>
        {
            new() { Id = Guid.NewGuid(), Nome = "Notebook", Preco = 3500m },
            new() { Id = Guid.NewGuid(), Nome = "Mouse",    Preco = 150m  }
        };

            _repoMock.Setup(r => r.GetAll()).ReturnsAsync(produtos);

            // Act
            var resultado = await _service.GetAll();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count);
            Assert.Contains(resultado, p => p.Nome == "Notebook");
        }

        [Fact]
        public async Task Create_DeveGerarNovoId()
        {
            // Arrange
            Produto? produtoCriado = null;

            _repoMock.Setup(r => r.Add(It.IsAny<Produto>()))

                     .Callback<Produto>(p => produtoCriado = p)
                     .Returns(Task.CompletedTask);

            var dto = new ProdutoCreateDTO { Nome = "Headset", Preco = 500m, Estoque = 5 };

            // Act
            await _service.Create(dto);

            // Assert
            Assert.NotNull(produtoCriado);
            Assert.NotEqual(Guid.Empty, produtoCriado.Id);
        }

        [Fact]
        public async Task Update_ProdutoExistente_DeveAtualizarNomeEPreco()
        {
            // Arrange
            var id = Guid.NewGuid();
            var produto = new Produto { Id = id, Nome = "Antigo", Preco = 100m };

            _repoMock.Setup(r => r.GetById(id)).ReturnsAsync(produto);
            _repoMock.Setup(r => r.Update(It.IsAny<Produto>())).Returns(Task.CompletedTask);

            var dto = new ProdutoUpdateDTO { Nome = "  Novo Nome  ", Preco = 999m };

            // Act
            await _service.Update(id, dto);

            // Assert
            Assert.Equal("Novo Nome", produto.Nome);
            Assert.Equal(999m, produto.Preco);

            _repoMock.Verify(r => r.Update(produto), Times.Once);
        }

        [Fact]
        public async Task Delete_ProdutoInexistente_DeveLancarNotFoundException()
        {
            // Arrange 
            _repoMock.Setup(r => r.GetById(It.IsAny<Guid>()))
                     .ReturnsAsync((Produto?)null);

            // Act
            await Assert.ThrowsAsync<NotFoundException>(
                () => _service.Delete(Guid.NewGuid())
            );
        }
    }
}