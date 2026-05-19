using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.Models;

namespace ApiEcommerce.Features.Api.Carrinhos.Repositories
{
    public interface ICarrinhoRepository
    {
        Task<List<Carrinho>> GetAll();
        Task<Carrinho?> GetById(Guid id, bool asNoTracking = true);
        Task Add(Carrinho carrinho);
        Task Update(Carrinho carrinho);
        Task Delete(Guid id);
    }
}