using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.Models;

namespace ApiEcommerce.Features.Api.Produtos.Repositories
{
    public interface IProdutoRepository
    {
        Task<List<Produto>> GetAll();
        Task<Produto?> GetById(Guid id);
        Task Add(Produto produto);
        Task Update(Produto produto);
        Task Delete(Guid id);
    }
}