using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.Data;
using ApiEcommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEcommerce.Repositories
{
    public class ProdutoRepository
    {
        private readonly ConnectionFactory _context;

        public ProdutoRepository(ConnectionFactory context)
        {
            _context = context;
        }

        public async Task<List<Produto>> GetAll()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }

        public async Task<Produto?> GetById(Guid id)
        {
            return await _context
                .Produtos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Add(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                throw new Exception("Produto não encontrado");
            }
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }

    }
}