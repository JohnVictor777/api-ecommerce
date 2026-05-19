using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.Models;

namespace ApiEcommerce.Features.Api.Usuarios.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAll();
        Task<Usuario?> GetById(Guid id);
        Task Add(Usuario usuario);
        Task Update(Usuario usuario);
        Task Delete(Guid id);
    }
}