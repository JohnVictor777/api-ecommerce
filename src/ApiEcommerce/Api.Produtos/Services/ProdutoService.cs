using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.DTOs;
using ApiEcommerce.Models;
using ApiEcommerce.Repositories;

namespace ApiEcommerce.Services
{
    public class ProdutoService
    {
        private readonly ProdutoRepositorie _repositorie;

        public ProdutoService(ProdutoRepositorie repositorie)
        {
            _repositorie = repositorie;
        }

        public async Task<List<ProdutoResponseDTO>> GetAll()
        {
            var produtos = await _repositorie.GetAll();

            return produtos.Select(p => new ProdutoResponseDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Preco = p.Preco
            }).ToList();
        }

        public async Task Create(ProdutoCreateDTO dto)
        {

            var produto = new Produto
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                Preco = dto.Preco
            };

            await _repositorie.Add(produto);
        }

        public async Task Delete(Guid id)
        {
            await _repositorie.Delete(id);
        }
    }
}