using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return await _context.Carrinhos.ToListAsync();
        }
        public async Task Add(Carrinho carrinho)
        {
            await _context.Carrinhos.AddAsync(carrinho);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var carrinho = await _context.Carrinhos.FindAsync(id);

            if (carrinho == null)
            {
                throw new Exception("Carrinho não encontrado");
            }

            _context.Carrinhos.Remove(carrinho);
            await _context.SaveChangesAsync();
        }

    }
}