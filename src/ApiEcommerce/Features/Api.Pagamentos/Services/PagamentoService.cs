using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.Features.Api.Pagamentos.DTOs.Create;
using ApiEcommerce.Features.Api.Pagamentos.DTOs.Response;
using ApiEcommerce.Features.Api.Pagamentos.DTOs.Update;
using ApiEcommerce.Features.Api.Pagamentos.Repositories;
using ApiEcommerce.Models;
using static ApiEcommerce.Models.Enum;

namespace ApiEcommerce.Features.Api.Pagamentos.Services
{
    public class PagamentoService
    {
        private readonly PagamentoRepository _repository;

        public PagamentoService(PagamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PagamentoResponseDTO>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<PagamentoResponseDTO?> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<PagamentoResponseDTO> Create(PagamentoCreateDTO dto)
        {
            var pagamento = new Pagamento
            {
                Id = Guid.NewGuid(),
                PedidoId = dto.PedidoId,
                Valor = 0,
                Status = StatusPagamento.Pendente,
                CriadoEm = DateTime.UtcNow
            };

            await _repository.Add(pagamento);
            return ToDTO(pagamento);
        }

        public async Task<bool> Update(Guid id, PagamentoUpdateDTO dto)
        {
            var pagamento = await _repository.GetEntityById(id);
            if (pagamento == null)
                return false;

            pagamento.Status = dto.Status;
            pagamento.AtualizadoEm = DateTime.UtcNow;

            await _repository.Update(pagamento);
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var pagamento = await _repository.GetEntityById(id);
            if (pagamento == null)
                return false;

            await _repository.Delete(pagamento);
            return true;
        }

        private static PagamentoResponseDTO ToDTO(Pagamento p) => new()
        {
            Id = p.Id,
            PedidoId = p.PedidoId,
            Valor = p.Valor,
            Status = p.Status,
            CriadoEm = p.CriadoEm,
            AtualizadoEm = p.AtualizadoEm
        };
    }
}