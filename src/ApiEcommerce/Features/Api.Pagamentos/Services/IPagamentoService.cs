using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.Features.Api.Pagamentos.DTOs.Create;
using ApiEcommerce.Features.Api.Pagamentos.DTOs.Response;
using ApiEcommerce.Features.Api.Pagamentos.DTOs.Update;

namespace ApiEcommerce.Features.Api.Pagamentos.Services
{
    public interface IPagamentoService
    {
        Task<List<PagamentoResponseDTO>> GetAll();
        Task<PagamentoResponseDTO?> GetById(Guid id);
        Task<PagamentoResponseDTO> Create(PagamentoCreateDTO dto);
        Task<bool> Update(Guid id, PagamentoUpdateDTO dto);
        Task<bool> Delete(Guid id);

    }
}