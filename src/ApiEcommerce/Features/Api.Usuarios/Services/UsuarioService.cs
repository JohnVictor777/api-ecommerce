using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.DTOs;
using ApiEcommerce.Features.Api.Usuarios.DTOs.Update;
using ApiEcommerce.Models;
using ApiEcommerce.Repositories;
using ApiEcommerce.Shared.Exceptions;

namespace ApiEcommerce.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repository;

        public UsuarioService(UsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UsuarioResponseDTO>> GetAll()
        {
            var usuarios = await _repository.GetAll();

            return usuarios.Select(u => new UsuarioResponseDTO
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email
            }).ToList();
        }

        public async Task<UsuarioResponseDTO?> GetById(Guid id)
        {
            var usuario = await _repository.GetById(id);
            return usuario == null ? null : new UsuarioResponseDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
        }

        public async Task Create(UsuarioCreateDTO dto)
        {
            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha
            };

            await _repository.Add(usuario);
        }

        public async Task Update(Guid id, UsuarioUpdateDTO dto)
        {
            var usuario = await _repository.GetById(id);
            if (usuario == null)
                throw new NotFoundException("Usuário não encontrado");

            usuario.Nome = dto.Nome.Trim();
            usuario.Email = dto.Email.Trim();
            usuario.Senha = dto.Senha?.Trim() ?? usuario.Senha;

            await _repository.Update(usuario);
        }

        public async Task Delete(Guid id)
        {
            var usuario = await _repository.GetById(id);
            if (usuario == null)
                throw new NotFoundException("Usuário não encontrado");

            await _repository.Delete(id);
        }
    }
}