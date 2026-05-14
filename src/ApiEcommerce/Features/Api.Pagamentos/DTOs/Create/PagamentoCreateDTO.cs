using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEcommerce.Features.Api.Pagamentos.DTOs.Create
{
    public class PagamentoCreateDTO
    {

        [Required(ErrorMessage = "O pedido é obrigatório")]
        public Guid PedidoId { get; set; }
    }
}