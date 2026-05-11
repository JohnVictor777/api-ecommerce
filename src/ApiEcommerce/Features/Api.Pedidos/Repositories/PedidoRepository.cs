using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.Data;
using ApiEcommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEcommerce.Repositories
{
    public class PedidoRepository
    {
        private readonly ConnectionFactory _context;

        public PedidoRepository(ConnectionFactory context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> GetAll()
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .ToListAsync();
        }

        public async Task Add(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                throw new Exception("Pedido não encontrado");
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

        }

    }
}