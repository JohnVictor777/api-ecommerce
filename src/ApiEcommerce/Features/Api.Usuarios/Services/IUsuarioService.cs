using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.DTOs;
using ApiEcommerce.Features.Api.Usuarios.DTOs.Update;

namespace ApiEcommerce.Features.Api.Usuarios.Services
{
    public interface IUsuarioService
    {
        Task<List<UsuarioResponseDTO>> GetAll();
        Task<UsuarioResponseDTO?> GetById(Guid id);
        Task Create(UsuarioCreateDTO usuario);
        Task Update(Guid id, UsuarioUpdateDTO usuario);
        Task Delete(Guid id);
    }
}