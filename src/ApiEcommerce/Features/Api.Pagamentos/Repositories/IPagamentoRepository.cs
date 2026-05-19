using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.Features.Api.Pagamentos.DTOs.Response;
using ApiEcommerce.Models;

namespace ApiEcommerce.Features.Api.Pagamentos.Repositories
{
    public interface IPagamentoRepository
    {
        Task<List<PagamentoResponseDTO>> GetAll();
        Task<PagamentoResponseDTO?> GetById(Guid id);
        Task Add(Pagamento pagamento);
        Task Update(Pagamento pagamento);
        Task Delete(Guid id);

    }
}