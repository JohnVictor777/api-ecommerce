using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.DTOs;
using ApiEcommerce.Features.Api.Usuarios.DTOs.Update;
using ApiEcommerce.Features.Api.Usuarios.Repositories;
using ApiEcommerce.Features.Api.Usuarios.Services;
using ApiEcommerce.Models;
using ApiEcommerce.Repositories;
using ApiEcommerce.Shared;
using ApiEcommerce.Shared.Exceptions;

namespace ApiEcommerce.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UsuarioResponseDTO>> GetAll()
        {
            var usuarios = await _repository.GetAll();

            return usuarios.Select(ToDTO).ToList();
        }

        public async Task<UsuarioResponseDTO?> GetById(Guid id)
        {
            var usuario = await _repository.GetById(id);
            return usuario == null ? null : ToDTO(usuario);
        }

        public async Task<UsuarioResponseDTO> Create(UsuarioCreateDTO dto)
        {
            var email = dto.Email.Trim();

            if (await _repository.GetByEmail(email) != null)
                throw new ValidationException("E-mail já cadastrado");

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome.Trim(),
                Email = email,
                Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
                Role = Roles.User
            };

            await _repository.Add(usuario);

            return ToDTO(usuario);
        }

        public async Task Update(Guid id, UsuarioUpdateDTO dto)
        {
            var usuario = await _repository.GetById(id);
            if (usuario == null)
                throw new NotFoundException("Usuário não encontrado");

            var email = dto.Email.Trim();
            var usuarioComEmail = await _repository.GetByEmail(email);
            if (usuarioComEmail != null && usuarioComEmail.Id != id)
                throw new ValidationException("E-mail já cadastrado");

            usuario.Nome = dto.Nome.Trim();
            usuario.Email = email;

            if (!string.IsNullOrWhiteSpace(dto.Senha))
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

            await _repository.Update(usuario);
        }

        public async Task Delete(Guid id)
        {
            var usuario = await _repository.GetById(id);
            if (usuario == null)
                throw new NotFoundException("Usuário não encontrado");

            await _repository.Delete(id);
        }

        private static UsuarioResponseDTO ToDTO(Usuario usuario) => new()
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Role = usuario.Role
        };
    }
}
