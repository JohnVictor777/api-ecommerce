using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.Data;
using ApiEcommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEcommerce.Repositories
{
    public class ProdutoRepositorie
    {
        private readonly ConnectionFactory _context;

        public ProdutoRepositorie(ConnectionFactory context)
        {
            _context = context;
        }

        public async Task<List<Produto>> GetAll()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task Add(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
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