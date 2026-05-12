using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.DTOs;
using ApiEcommerce.Repositories;
using Microsoft.EntityFrameworkCore;
using ApiEcommerce.Models;
using ApiEcommerce.Features.Api.Pedidos.DTOs.Update;
using static ApiEcommerce.Models.Enum;

namespace ApiEcommerce.Services
{
    public class PedidoService
    {
        private readonly PedidoRepository _repository;
        private readonly ProdutoRepository _produtoRepositorie;

        public PedidoService(PedidoRepository repository, ProdutoRepository produtoRepositorie)
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

        public async Task<PedidoResponseDTO?> GetById(Guid id)
        {
            var pedido = await _repository.GetById(id);

            if (pedido == null)
                return null;

            return new PedidoResponseDTO
            {
                Id = pedido.Id,
                DataPedido = pedido.DataPedido,
                Total = pedido.Total,
                Status = pedido.Status,

                Itens = pedido.Itens.Select(i => new ItemPedidoResponseDTO
                {
                    ProdutoNome = i.Produto?.Nome ?? "Produto removido",
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario,
                    ValorTotal = i.Quantidade * i.PrecoUnitario
                }).ToList()
            };
        }

        public async Task Create(PedidoCreateDTO dto)
        {
            var itens = new List<Models.ItemPedido>();

            foreach (var itemDto in dto.Itens)
            {
                var produto = await _produtoRepositorie.GetById(itemDto.ProdutoId);

                if (produto == null)
                    throw new Exception("Produto não encontrado");

                itens.Add(new ItemPedido
                {
                    Id = Guid.NewGuid(),
                    ProdutoId = produto.Id,
                    Quantidade = itemDto.Quantidade,
                    PrecoUnitario = produto.Preco
                });
            }

            var pedido = new Pedido
            {
                Id = Guid.NewGuid(),
                DataPedido = DateTime.UtcNow,
                Status = Models.Enum.StatusPedido.Pendente,
                Itens = itens,
                Total = itens.Sum(i => i.Quantidade * i.PrecoUnitario)
            };

            await _repository.Add(pedido);
        }

        public async Task Update(Guid id, PedidoUpdateDTO dto)
        {
            var pedido = await _repository.GetById(id);
            if (pedido == null)
                throw new KeyNotFoundException("Pedido não encontrado");

            if (!System.Enum.IsDefined(typeof(StatusPedido), dto.Status))
            {
                throw new Exception("Status inválido");
            }

            pedido.Status = dto.Status;
            await _repository.Update(pedido);
        }

        public async Task Delete(Guid id)
        {
            await _repository.Delete(id);
        }

    }
}