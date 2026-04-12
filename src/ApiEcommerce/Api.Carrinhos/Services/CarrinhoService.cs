using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.DTOs;
using ApiEcommerce.Repositories;
using Microsoft.EntityFrameworkCore;
using ApiEcommerce.Models;


namespace ApiEcommerce.Services
{
    public class CarrinhoService
    {
        private readonly CarrinhoRepository _repository;
        private readonly ProdutoRepositorie _produtoRepositorie;

        public CarrinhoService(CarrinhoRepository repository, ProdutoRepositorie produtoRepositorie)
        {
            _repository = repository;
            _produtoRepositorie = produtoRepositorie;
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
                var produto = await _produtoRepositorie.GetById(itemDto.ProdutoId);

                if (produto == null)
                    throw new Exception("Produto não encontrado");

                carrinho.Itens.Add(new ItemCarrinho
                {
                    Id = Guid.NewGuid(),
                    ProdutoId = produto.Id,
                    Quantidade = itemDto.Quantidade <= 0 ? 1 : itemDto.Quantidade,
                    PrecoUnitario = produto.Preco,
                    CarrinhoId = carrinho.Id
                });
            }

            await _repository.Add(carrinho);
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
        public async Task Delete(Guid id)
        {
            await _repository.Delete(id);

        }
    }
}