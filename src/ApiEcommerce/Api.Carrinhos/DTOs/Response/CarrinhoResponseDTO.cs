using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEcommerce.DTOs
{
    public class CarrinhoResponseDTO
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public List<ItemCarrinhoResponseDTO> Itens { get; set; } = new();
        public decimal Total { get; set; }
    }
}