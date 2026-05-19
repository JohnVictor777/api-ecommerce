using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.DTOs;
using ApiEcommerce.Features.Api.Pedidos.DTOs.Update;

namespace ApiEcommerce.Features.Api.Pedidos.Services
{
    public interface IPedidoService
    {
        Task<List<PedidoResponseDTO>> GetAll();
        Task<PedidoResponseDTO?> GetById(Guid id);
        Task Create(PedidoCreateDTO pedido);
        Task Update(Guid id, PedidoUpdateDTO pedido);
        Task Delete(Guid id);
    }
}