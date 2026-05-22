using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.Features.Api.Pagamentos.DTOs.Create;
using ApiEcommerce.Features.Api.Pagamentos.DTOs.Response;
using ApiEcommerce.Features.Api.Pagamentos.DTOs.Update;
using ApiEcommerce.Features.Api.Pagamentos.Repositories;
using ApiEcommerce.Features.Api.Pagamentos.Services;
using ApiEcommerce.Models;
using Moq;
using Xunit;
using static ApiEcommerce.Models.Enum;

namespace ApiEcommerce.Tests.Features.Test.Pagamentos
{
    public class PagamentoServiceTests
    {

        private readonly Mock<IPagamentoRepository> _repoMock;
        private readonly IPagamentoService _service;

        public PagamentoServiceTests()
        {
            _repoMock = new Mock<IPagamentoRepository>();
            _service = new PagamentoService(_repoMock.Object);
        }

        [Fact]
        public async Task Create_DeveCriarPagamento()
        {
            // Arrange
            var dto =
                new PagamentoCreateDTO
                {
                    PedidoId =
                        Guid.NewGuid()
                };

            // Act
            var resultado =
                await _service.Create(dto);

            // Assert
            Assert.NotNull(resultado);

            Assert.Equal(
                StatusPagamento.Pendente,
                resultado.Status);

            Assert.Equal(
                dto.PedidoId,
                resultado.PedidoId);

            _repoMock.Verify(
                x => x.Add(It.IsAny<Pagamento>()),
                Times.Once);
        }

        [Fact]
        public async Task GetById_DeveRetornarPagamento()
        {
            // Arrange
            var id =
                Guid.NewGuid();

            var pagamento =
                new PagamentoResponseDTO
                {
                    Id = id,
                    Valor = 150,
                    Status =
                        StatusPagamento.Aprovado
                };

            _repoMock
                .Setup(x => x.GetById(id))
                .ReturnsAsync(pagamento);

            // Act
            var resultado =
                await _service.GetById(id);

            // Assert
            Assert.NotNull(resultado);

            Assert.Equal(
                150,
                resultado!.Valor);
        }

        [Fact]
        public async Task Update_DeveRetornarFalse_QuandoPagamentoNaoExiste()
        {
            // Arrange
            var id =
                Guid.NewGuid();

            _repoMock
                .Setup(x => x.GetById(id))
                .ReturnsAsync(
                    (PagamentoResponseDTO?)null);

            var dto =
                new PagamentoUpdateDTO
                {
                    Status =
                        StatusPagamento.Aprovado
                };

            // Act
            var resultado =
                await _service.Update(id, dto);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task Update_DeveAtualizarPagamento()
        {
            // Arrange
            var id =
                Guid.NewGuid();

            _repoMock
                .Setup(x => x.GetById(id))
                .ReturnsAsync(
                    new PagamentoResponseDTO
                    {
                        Id = id,
                        PedidoId =
                            Guid.NewGuid(),

                        Valor = 100,

                        Status =
                            StatusPagamento.Pendente,

                        CriadoEm =
                            DateTime.UtcNow
                    });

            var dto =
                new PagamentoUpdateDTO
                {
                    Status =
                        StatusPagamento.Aprovado
                };

            // Act
            var resultado =
                await _service.Update(id, dto);


            // Assert
            Assert.True(resultado);

            _repoMock.Verify(
                x => x.Update(
                    It.IsAny<Pagamento>()),
                Times.Once);
        }

        [Fact]
        public async Task Delete_DeveRetornarFalse_QuandoNaoExiste()
        {
            // Arrange

            var id =
                Guid.NewGuid();

            _repoMock
                .Setup(x => x.GetById(id))
                .ReturnsAsync(
                    (PagamentoResponseDTO?)null);

            // Act
            var resultado =
                await _service.Delete(id);

            // Assert

            Assert.False(resultado);
        }

        [Fact]
        public async Task Delete_DeveExcluirPagamento()
        {
            // Arrange
            var id =
                Guid.NewGuid();

            _repoMock
                .Setup(x => x.GetById(id))
                .ReturnsAsync(
                    new PagamentoResponseDTO
                    {
                        Id = id
                    });

            // Act
            var resultado =
                await _service.Delete(id);

            // Assert
            Assert.True(resultado);

            _repoMock.Verify(
                x => x.Delete(id),
                Times.Once);
        }
    }
}