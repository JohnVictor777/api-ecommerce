using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.Data;
using ApiEcommerce.Features.Api.Pagamentos.DTOs.Response;
using ApiEcommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEcommerce.Features.Api.Pagamentos.Repositories
{
    public class PagamentoRepository
    {

        private readonly ConnectionFactory _context;

        public PagamentoRepository(ConnectionFactory context)
        {
            _context = context;
        }

        public async Task<List<PagamentoResponseDTO>> GetAll()
        {
            return await _context.Pagamentos
                .AsNoTracking()
                .Select(p => new PagamentoResponseDTO
                {
                    Id = p.Id,
                    PedidoId = p.PedidoId,
                    Valor = p.Valor,
                    Status = p.Status,
                    CriadoEm = p.CriadoEm,
                    AtualizadoEm = p.AtualizadoEm
                })
                .ToListAsync();
        }

        public async Task<PagamentoResponseDTO?> GetById(Guid id)
        {
            return await _context.Pagamentos
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new PagamentoResponseDTO
                {
                    Id = p.Id,
                    PedidoId = p.PedidoId,
                    Valor = p.Valor,
                    Status = p.Status,
                    CriadoEm = p.CriadoEm,
                    AtualizadoEm = p.AtualizadoEm
                })
                .FirstOrDefaultAsync();
        }

        public async Task Add(Pagamento pagamento)
        {
            await _context.Pagamentos.AddAsync(pagamento);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Pagamento pagamento)
        {
            _context.Pagamentos.Update(pagamento);
            await _context.SaveChangesAsync();
        }

        public async Task<Pagamento?> GetEntityById(Guid id)
        {
            return await _context.Pagamentos
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Delete(Pagamento pagamento)
        {
            _context.Pagamentos.Remove(pagamento);
            await _context.SaveChangesAsync();
        }
    }
}