using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.Data;
using ApiEcommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEcommerce.Repositories
{
    public class UsuarioRepository
    {
        private readonly ConnectionFactory _context;

        public UsuarioRepository(ConnectionFactory context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> GetAll()
        {
            return await _context.Usuarios.AsNoTracking().ToListAsync();
        }

        public async Task<Usuario?> GetById(Guid id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task Add(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}