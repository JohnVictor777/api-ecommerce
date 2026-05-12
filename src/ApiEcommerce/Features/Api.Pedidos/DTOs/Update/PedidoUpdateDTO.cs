using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiEcommerce.Models.Enum;

namespace ApiEcommerce.Features.Api.Pedidos.DTOs.Update
{
    public class PedidoUpdateDTO
    {
        public StatusPedido Status { get; set; }
    }
}