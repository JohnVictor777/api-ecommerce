using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.Models;

namespace ApiEcommerce.Features.Api.Pedidos.Repositories
{
    public interface IPedidoRepository
    {
        Task<List<Pedido>> GetAll();
        Task<Pedido?> GetById(Guid id);
        Task Add(Pedido pedido);
        Task Update(Pedido pedido);
        Task Delete(Guid id);
    }
}