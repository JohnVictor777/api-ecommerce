using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.Data;
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

        public async Task<List<Pagamento>> GetAll()
        {
            return await _context.Pagamentos
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Pagamento?> GetById(Guid id)
        {
            return await _context.Pagamentos
                .FirstOrDefaultAsync(p => p.Id == id);
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

        public async Task Delete(Pagamento pagamento)
        {
            _context.Pagamentos.Remove(pagamento);
            await _context.SaveChangesAsync();
        }
    }
}