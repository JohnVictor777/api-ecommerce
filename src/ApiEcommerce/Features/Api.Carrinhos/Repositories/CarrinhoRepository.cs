using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.Shared.Exceptions;
using ApiEcommerce.Data;
using ApiEcommerce.Models;
using Microsoft.EntityFrameworkCore;


namespace ApiEcommerce.Repositories
{
    public class CarrinhoRepository
    {
        private readonly ConnectionFactory _context;

        public CarrinhoRepository(ConnectionFactory context)
        {
            _context = context;
        }
        public async Task<List<Carrinho>> GetAll()
        {
            return await _context.Carrinhos
                .Include(c => c.Itens)
                .ThenInclude(i => i.Produto)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Carrinho?> GetById(Guid id, bool asNoTracking = true)
        {
            IQueryable<Carrinho> query = _context.Carrinhos
                .Include(c => c.Itens)
                .ThenInclude(i => i.Produto);

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var carrinho = await query.FirstOrDefaultAsync(c => c.Id == id);

            return carrinho;
        }

        public async Task Add(Carrinho carrinho)
        {
            await _context.Carrinhos.AddAsync(carrinho);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Carrinho carrinho)
        {
            var itensAntigos = _context.ItensCarrinho
                .Where(i => i.CarrinhoId == carrinho.Id);

            _context.ItensCarrinho.RemoveRange(itensAntigos);

            await _context.ItensCarrinho.AddRangeAsync(carrinho.Itens);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var carrinho = await _context.Carrinhos.FindAsync(id);

            if (carrinho == null)
                throw new NotFoundException("Carrinho não encontrado");

            _context.Carrinhos.Remove(carrinho);
            await _context.SaveChangesAsync();
        }
    }
}