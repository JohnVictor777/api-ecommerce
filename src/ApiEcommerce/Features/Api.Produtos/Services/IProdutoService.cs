using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.DTOs;
using ApiEcommerce.Features.Api.Produtos.DTOs.Update;

namespace ApiEcommerce.Features.Api.Produtos.Services
{
    public interface IProdutoService
    {
        Task<List<ProdutoResponseDTO>> GetAll();
        Task<ProdutoResponseDTO?> GetById(Guid id);
        Task Create(ProdutoCreateDTO dto);
        Task Update(Guid id, ProdutoUpdateDTO dto);
        Task Delete(Guid id);
    }
}