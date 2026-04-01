using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.DTOs;
using ApiEcommerce.Models;
using ApiEcommerce.Repositories;

namespace ApiEcommerce.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepositorie _repositorie;

        public UsuarioService(UsuarioRepositorie repositorie)
        {
            _repositorie = repositorie;
        }

        public async Task<List<UsuarioResponseDTO>> GetAll()
        {
            var usuarios = await _repositorie.GetAll();

            return usuarios.Select(u => new UsuarioResponseDTO
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email
            }).ToList();
        }

        public async Task Create(UsuarioCreateDTO dto)
        {
            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = dto.SenhaHash
            };

            await _repositorie.Add(usuario);
        }

        public async Task Delete(Guid id)
        {
            await _repositorie.Delete(id);
        }
    }
}