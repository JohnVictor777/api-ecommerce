using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiEcommerce.Models.Enum;

namespace ApiEcommerce.Features.Api.Pagamentos.DTOs.Response
{
    public class PagamentoResponseDTO
    {
        public Guid Id { get; set; }

        public Guid PedidoId { get; set; }

        public decimal Valor { get; set; }

        public StatusPagamento Status { get; set; }

        public DateTime CriadoEm { get; set; }

        public DateTime? AtualizadoEm { get; set; }
    }
}