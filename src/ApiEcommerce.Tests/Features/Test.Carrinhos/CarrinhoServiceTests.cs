using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.DTOs;
using ApiEcommerce.Features.Api.Carrinhos.Repositories;
using ApiEcommerce.Features.Api.Carrinhos.Services;
using ApiEcommerce.Features.Api.Produtos.Repositories;
using ApiEcommerce.Models;
using ApiEcommerce.Services;
using ApiEcommerce.Shared.Exceptions;
using Moq;
using Xunit;

namespace ApiEcommerce.Tests.Features.Test.Carrinhos
{
    public class CarrinhoServiceTests
    {

        private readonly Mock<ICarrinhoRepository> _carrinhoRepository;
        private readonly Mock<IProdutoRepository> _produtoRepository;
        private readonly CarrinhoService _service;

        public CarrinhoServiceTests()
        {
            _carrinhoRepository =
                new Mock<ICarrinhoRepository>();

            _produtoRepository =
                new Mock<IProdutoRepository>();

            _service =
                new CarrinhoService(
                    _carrinhoRepository.Object,
                    _produtoRepository.Object);
        }

        [Fact]
        public async Task Create_DeveCriarCarrinho()
        {
            // Arrange
            var produtoId =
                Guid.NewGuid();

            _produtoRepository
                .Setup(x =>
                    x.GetById(produtoId))

                .ReturnsAsync(
                    new Produto
                    {
                        Id = produtoId,
                        Preco = 50
                    });

            var dto =
                new CarrinhoCreateDTO
                {
                    UsuarioId =
                        Guid.NewGuid(),

                    Itens =
                    [
                        new ItemCarrinhoCreateDTO
                    {
                        ProdutoId =
                            produtoId,

                        Quantidade = 2
                    }
                    ]
                };


            // Act
            await _service.Create(dto);

            // Assert
            _carrinhoRepository.Verify(
                x => x.Add(It.IsAny<Carrinho>()),
                Times.Once);
        }

        [Fact]
        public async Task Create_DeveLancarErro_ProdutoNaoExiste()
        {
            // Arrange
            var dto =
                new CarrinhoCreateDTO
                {
                    UsuarioId =
                        Guid.NewGuid(),

                    Itens =
                    [
                        new ItemCarrinhoCreateDTO
                    {
                        ProdutoId =
                            Guid.NewGuid(),
                        Quantidade = 1
                    }
                    ]
                };

            // Act + Assert
            await Assert.ThrowsAsync<
                NotFoundException>(() =>
                    _service.Create(dto));
        }

        [Fact]
        public async Task GetById_DeveRetornarCarrinho()
        {
            // Arrange
            var id =
                Guid.NewGuid();

            _carrinhoRepository
                .Setup(x =>
                    x.GetById(id, true))

                .ReturnsAsync(
                    new Carrinho
                    {
                        Id = id,

                        Itens =
                        [
                            new ItemCarrinho
                        {
                            Quantidade = 2,
                            PrecoUnitario = 100
                        }
                        ]
                    });


            // Act
            var resultado =
                await _service.GetById(id);

            // Assert
            Assert.Equal(
                200,
                resultado.Total);
        }

        [Fact]
        public async Task Delete_DeveExcluirCarrinho()
        {
            // Arrange
            var id =
                Guid.NewGuid();

            _carrinhoRepository
                .Setup(x =>
                    x.GetById(id, true))

                .ReturnsAsync(
                    new Carrinho
                    {
                        Id = id
                    });

            // Act
            await _service.Delete(id);

            // Assert
            _carrinhoRepository.Verify(
                x => x.Delete(id),
                Times.Once);
        }

        [Fact]
        public async Task Delete_DeveLancarErro()
        {
            // Arrange
            var id =
                Guid.NewGuid();

            _carrinhoRepository
                .Setup(x =>
                    x.GetById(id, true))

                .ReturnsAsync(
                    (Carrinho?)null);

            // Act + Assert
            await Assert.ThrowsAsync<
                NotFoundException>(() =>
                    _service.Delete(id));
        }
    }
}