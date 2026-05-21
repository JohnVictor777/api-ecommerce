using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.DTOs;
using ApiEcommerce.Features.Api.Pedidos.DTOs.Update;
using ApiEcommerce.Features.Api.Pedidos.Repositories;
using ApiEcommerce.Features.Api.Pedidos.Services;
using ApiEcommerce.Features.Api.Produtos.Repositories;
using ApiEcommerce.Models;
using ApiEcommerce.Services;
using ApiEcommerce.Shared.Exceptions;
using Moq;
using static ApiEcommerce.Models.Enum;

namespace ApiEcommerce.Tests.Features.Pedidos
{
    public class PedidoServiceTests
    {
        private readonly Mock<IPedidoRepository> _pedidoRepoMock;
        private readonly Mock<IProdutoRepository> _produtoRepoMock;

        private readonly IPedidoService _service;

        public PedidoServiceTests()
        {
            _pedidoRepoMock = new Mock<IPedidoRepository>();
            _produtoRepoMock = new Mock<IProdutoRepository>();

            _service = new PedidoService(
                _pedidoRepoMock.Object,
                _produtoRepoMock.Object
            );

        }

        [Fact]
        public async Task Create_DeveCalcularTotalCorretamente()
        {
            // Arrange
            Pedido? pedidoCriado = null;

            var produto = new Produto
            {
                Id = Guid.NewGuid(),
                Nome = "Notebook",
                Preco = 3000m
            };

            _produtoRepoMock
                .Setup(r => r.GetById(produto.Id))
                .ReturnsAsync(produto);

            _pedidoRepoMock
                .Setup(r => r.Add(It.IsAny<Pedido>()))
                .Callback<Pedido>(p => pedidoCriado = p)
                .Returns(Task.CompletedTask);

            var dto = new PedidoCreateDTO
            {
                Itens = new List<ItemPedidoCreateDTO>
        {
            new()
            {
                ProdutoId = produto.Id,
                Quantidade = 2
            }
        }
            };

            // Act
            await _service.Create(dto);

            // Assert
            Assert.NotNull(pedidoCriado);

            Assert.Equal(6000m, pedidoCriado.Total);

            Assert.Single(pedidoCriado.Itens);

            Assert.Equal(
                StatusPedido.Pendente,
                pedidoCriado.Status
            );

            _pedidoRepoMock.Verify(
                r => r.Add(It.IsAny<Pedido>()),
                Times.Once
            );
        }

        [Fact]
        public async Task Create_ProdutoInexistente_DeveLancarNotFoundException()
        {
            // Arrange
            _produtoRepoMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .ReturnsAsync((Produto?)null);

            var dto = new PedidoCreateDTO
            {
                Itens = new List<ItemPedidoCreateDTO>
        {
            new()
            {
                ProdutoId = Guid.NewGuid(),
                Quantidade = 1
            }
        }
            };

            // Act + Assert
            await Assert.ThrowsAsync<NotFoundException>(
                () => _service.Create(dto)
            );
        }

        [Fact]
        public async Task Update_StatusInvalido_DeveLancarValidationException()
        {
            // Arrange
            var pedido = new Pedido
            {
                Id = Guid.NewGuid(),
                Status = StatusPedido.Pendente
            };

            _pedidoRepoMock
                .Setup(r => r.GetById(pedido.Id))
                .ReturnsAsync(pedido);

            var dto = new PedidoUpdateDTO
            {
                Status = (StatusPedido)999
            };

            // Act + Assert
            await Assert.ThrowsAsync<ValidationException>(
                () => _service.Update(pedido.Id, dto)
            );
        }

        [Fact]
        public async Task Update_PedidoInexistente_DeveLancarNotFoundException()
        {
            // Arrange
            _pedidoRepoMock
                .Setup(r => r.GetById(It.IsAny<Guid>()))
                .ReturnsAsync((Pedido?)null);

            var dto = new PedidoUpdateDTO
            {
                Status = StatusPedido.Entregue
            };

            // Act + Assert
            await Assert.ThrowsAsync<NotFoundException>(
                () => _service.Update(Guid.NewGuid(), dto)
            );
        }

        [Fact]
        public async Task Update_StatusValido_DeveAtualizarStatus()
        {
            // Arrange
            var pedido = new Pedido
            {
                Id = Guid.NewGuid(),
                Status = StatusPedido.Pendente
            };

            _pedidoRepoMock
                .Setup(r => r.GetById(pedido.Id))
                .ReturnsAsync(pedido);

            _pedidoRepoMock
                .Setup(r => r.Update(It.IsAny<Pedido>()))
                .Returns(Task.CompletedTask);

            var dto = new PedidoUpdateDTO
            {
                Status = StatusPedido.Entregue
            };

            // Act
            await _service.Update(pedido.Id, dto);

            // Assert
            Assert.Equal(StatusPedido.Entregue, pedido.Status);

            _pedidoRepoMock.Verify(
                r => r.Update(pedido),
                Times.Once
            );

        }

    }
}