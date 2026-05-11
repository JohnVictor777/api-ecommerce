using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.Models;
using static ApiEcommerce.Models.Enum;

namespace ApiEcommerce.DTOs
{
    public class PedidoResponseDTO
    {
        public Guid Id { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal Total { get; set; }
        public StatusPedido Status { get; set; }
        public List<ItemPedidoResponseDTO> Itens { get; set; } = new();
    }
}