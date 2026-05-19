using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.DTOs;
using ApiEcommerce.Features.Api.Carrinhos.DTOs.Update;

namespace ApiEcommerce.Features.Api.Carrinhos.Services
{
    public interface ICarrinhoService
    {
        Task<List<CarrinhoResponseDTO>> GetAll();
        Task<CarrinhoResponseDTO> GetById(Guid id);
        Task Create(CarrinhoCreateDTO dto);
        Task Update(Guid id, CarrinhoUpdateDTO dto);
        Task Delete(Guid id);
    }
}