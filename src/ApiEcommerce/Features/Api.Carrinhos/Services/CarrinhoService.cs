using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.DTOs;
using ApiEcommerce.Repositories;
using Microsoft.EntityFrameworkCore;
using ApiEcommerce.Models;
using ApiEcommerce.Features.Api.Carrinhos.DTOs.Update;
using ApiEcommerce.Shared.Exceptions;


namespace ApiEcommerce.Services
{
    public class CarrinhoService
    {
        private readonly CarrinhoRepository _repository;
        private readonly ProdutoRepository _produtoRepository;

        public CarrinhoService(CarrinhoRepository repository, ProdutoRepository produtoRepository)
        {
            _repository = repository;
            _produtoRepository = produtoRepository;
        }

        public async Task<List<CarrinhoResponseDTO>> GetAll()
        {
            var carrinhos = await _repository.GetAll();

            return carrinhos.Select(c => new CarrinhoResponseDTO
            {
                Id = c.Id,
                UsuarioId = c.UsuarioId,
                Itens = c.Itens.Select(i => new ItemCarrinhoResponseDTO
                {
                    ProdutoNome = i.Produto?.Nome ?? "Produto removido",
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario,
                    ValorTotal = i.Quantidade * i.PrecoUnitario
                }).ToList(),
                Total = c.Itens.Sum(i => i.Quantidade * i.PrecoUnitario)
            }).ToList();
        }

        public async Task<CarrinhoResponseDTO> GetById(Guid id)
        {
            var carrinho = await _repository.GetById(id);
            if (carrinho == null)
                throw new NotFoundException("Carrinho não encontrado");

            return new CarrinhoResponseDTO
            {
                Id = carrinho.Id,
                UsuarioId = carrinho.UsuarioId,
                Itens = carrinho.Itens.Select(i => new ItemCarrinhoResponseDTO
                {
                    ProdutoNome = i.Produto?.Nome ?? "Produto removido",
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario,
                    ValorTotal = i.Quantidade * i.PrecoUnitario
                }).ToList(),
                Total = carrinho.Itens.Sum(i => i.Quantidade * i.PrecoUnitario)
            };
        }

        public async Task Create(CarrinhoCreateDTO dto)
        {
            var carrinho = new Carrinho
            {
                Id = Guid.NewGuid(),
                UsuarioId = dto.UsuarioId,
                Itens = new List<ItemCarrinho>()
            };

            foreach (var itemDto in dto.Itens)
            {
                var produto = await _produtoRepository.GetById(itemDto.ProdutoId);

                if (produto == null)
                    throw new NotFoundException("Produto não encontrado");

                carrinho.Itens.Add(new ItemCarrinho
                {
                    Id = Guid.NewGuid(),
                    ProdutoId = produto.Id,
                    Quantidade = itemDto.Quantidade,
                    PrecoUnitario = produto.Preco,
                    CarrinhoId = carrinho.Id
                });
            }

            await _repository.Add(carrinho);
        }

        public async Task Update(Guid id, CarrinhoUpdateDTO dto)
        {
            var carrinho = await _repository.GetById(id, asNoTracking: false);

            if (carrinho == null)
                throw new NotFoundException("Carrinho não encontrado");

            var novosItens = new List<ItemCarrinho>();

            foreach (var itemDto in dto.Itens)
            {
                var produto = await _produtoRepository.GetById(itemDto.ProdutoId);

                if (produto == null)
                    throw new NotFoundException("Produto não encontrado");

                novosItens.Add(new ItemCarrinho
                {
                    Id = Guid.NewGuid(),
                    ProdutoId = itemDto.ProdutoId,
                    Quantidade = itemDto.Quantidade,
                    PrecoUnitario = produto.Preco,
                    CarrinhoId = carrinho.Id
                });
            }

            carrinho.Itens = novosItens;

            await _repository.Update(carrinho);
        }


        public async Task Delete(Guid id)
        {
            var carrinho = await _repository.GetById(id, asNoTracking: true);
            if (carrinho == null)
                throw new NotFoundException("Carrinho não encontrado");

            await _repository.Delete(id);
        }
    }
}