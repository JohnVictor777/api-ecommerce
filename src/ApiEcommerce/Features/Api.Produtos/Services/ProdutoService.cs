using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.DTOs;
using ApiEcommerce.Features.Api.Produtos.DTOs.Update;
using ApiEcommerce.Models;
using ApiEcommerce.Repositories;
using ApiEcommerce.Shared.Exceptions;

namespace ApiEcommerce.Services
{
    public class ProdutoService
    {
        private readonly ProdutoRepository _repository;

        public ProdutoService(ProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProdutoResponseDTO>> GetAll()
        {
            var produtos = await _repository.GetAll();

            return produtos.Select(p => new ProdutoResponseDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Preco = p.Preco
            }).ToList();
        }
        public async Task<ProdutoResponseDTO?> GetById(Guid id)
        {
            var produto = await _repository.GetById(id);
            return produto == null ? null : new ProdutoResponseDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco
            };
        }

        public async Task Create(ProdutoCreateDTO dto)
        {
            var produto = new Produto
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                Preco = dto.Preco,
                Estoque = dto.Estoque
            };
            await _repository.Add(produto);
        }

        public async Task Update(Guid id, ProdutoUpdateDTO dto)
        {
            var produto = await _repository.GetById(id);
            if (produto == null)
                throw new NotFoundException("Produto não encontrado");

            produto.Nome = dto.Nome.Trim();
            produto.Preco = dto.Preco;

            await _repository.Update(produto);
        }

        public async Task Delete(Guid id)
        {
            var produto = await _repository.GetById(id);
            if (produto == null)
                throw new NotFoundException("Produto não encontrado");

            await _repository.Delete(id);
        }

    }
}