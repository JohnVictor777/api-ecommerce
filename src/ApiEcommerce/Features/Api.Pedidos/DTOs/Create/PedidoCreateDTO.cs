using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ApiEcommerce.Models;
using static ApiEcommerce.Models.Enum;

namespace ApiEcommerce.DTOs
{
    public class PedidoCreateDTO
    {
        [Required(ErrorMessage = "Pelo menos um item é obrigatório para criar um pedido.")]
        [MinLength(1, ErrorMessage = "Pelo menos um item é obrigatório para criar um pedido.")]
        public List<ItemPedidoCreateDTO> Itens { get; set; } = new();
    }
}