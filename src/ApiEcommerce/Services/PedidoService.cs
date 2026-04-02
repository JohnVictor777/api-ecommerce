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
    public class PedidoService
    {
        private readonly PedidoRepository _repository;
        private readonly ProdutoRepositorie _produtoRepositorie;

        public PedidoService(PedidoRepository repository, ProdutoRepositorie produtoRepositorie)
        {
            _repository = repository;
            _produtoRepositorie = produtoRepositorie;
        }

        public async Task<List<PedidoResponseDTO>> GetAll()
        {
            var pedidos = await _repository.GetAll();

            return pedidos.Select(p => new PedidoResponseDTO
            {
                Id = p.Id,
                DataPedido = p.DataPedido,
                Total = p.Total,
                Status = p.Status,

                Itens = p.Itens.Select(i => new ItemPedidoResponseDTO
                {
                    ProdutoNome = i.Produto?.Nome ?? "Produto removido",
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario,
                    ValorTotal = i.Quantidade * i.PrecoUnitario
                }).ToList()

            }).ToList();
        }

        public async Task Create(PedidoCreateDTO dto)
        {
            var itens = new List<Models.ItemPedido>();

            foreach (var itemDto in dto.Itens)
            {
                var produto = await _produtoRepositorie.GetById(itemDto.ProdutoId);

                if (produto == null)
                    throw new Exception("Produto não encontrado");

                itens.Add(new Models.ItemPedido
                {
                    Id = Guid.NewGuid(),
                    ProdutoId = produto.Id,
                    Quantidade = itemDto.Quantidade <= 0
                                    ? throw new Exception("Quantidade deve ser maior que zero")
                                    : itemDto.Quantidade,
                    PrecoUnitario = produto.Preco
                });
            }

            var pedido = new Models.Pedido
            {
                Id = Guid.NewGuid(),
                DataPedido = DateTime.UtcNow,
                Status = Models.Enum.StatusPedido.Pendente,
                Itens = itens,
                Total = itens.Sum(i => i.Quantidade * i.PrecoUnitario)
            };

            await _repository.Add(pedido);
        }

        public async Task Delete(Guid id)
        {
            await _repository.Delete(id);
        }

    }
}