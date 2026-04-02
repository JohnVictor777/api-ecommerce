using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.Models;
using static ApiEcommerce.Models.Enum;

namespace ApiEcommerce.DTOs
{
    public class PedidoCreateDTO
    {
        public List<ItemPedidoCreateDTO> Itens { get; set; } = new();
    }
}