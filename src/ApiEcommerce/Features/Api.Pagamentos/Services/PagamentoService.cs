using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<Pagamento>> GetAll()
        {
            var pagamentos = await _repository.GetAll();

            return pagamentos.Select(p => new Pagamento
            {
                Id = p.Id,
                Valor = p.Valor,
                PedidoId = p.PedidoId,
                Status = p.Status,
                CriadoEm = p.CriadoEm,
                AtualizadoEm = p.AtualizadoEm
            }).ToList();

        }

        public async Task<Pagamento?> GetById(Guid id)
        {
            var pagamento = await _repository.GetById(id);
            return pagamento == null ? null : new Pagamento
            {
                Id = pagamento.Id,
                Valor = pagamento.Valor,
                PedidoId = pagamento.PedidoId,
                Status = pagamento.Status,
                CriadoEm = pagamento.CriadoEm,
                AtualizadoEm = pagamento.AtualizadoEm
            };
        }

        public async Task Create(Pagamento pagamento)
        {
            var newPagamento = new Pagamento
            {
                Id = Guid.NewGuid(),
                Valor = pagamento.Valor,
                PedidoId = pagamento.PedidoId,
                Status = pagamento.Status,
                CriadoEm = pagamento.CriadoEm,
                AtualizadoEm = pagamento.AtualizadoEm
            };

            await _repository.Add(newPagamento);
        }

        public async Task Update(Guid id, Pagamento pagamento)
        {
            var existingPagamento = await _repository.GetById(id);
            if (existingPagamento == null)
                return;

            existingPagamento.Valor = pagamento.Valor;
            existingPagamento.PedidoId = pagamento.PedidoId;
            existingPagamento.Status = pagamento.Status;
            existingPagamento.CriadoEm = pagamento.CriadoEm;
            existingPagamento.AtualizadoEm = pagamento.AtualizadoEm;

            await _repository.Update(existingPagamento);
        }

        public async Task Delete(Guid id)
        {
            var pagamento = await _repository.GetById(id);
            if (pagamento == null)
                return;

            await _repository.Delete(pagamento);

        }
    }
}