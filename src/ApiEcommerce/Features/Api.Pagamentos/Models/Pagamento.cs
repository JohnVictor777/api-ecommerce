using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiEcommerce.Models.Enum;

namespace ApiEcommerce.Models
{
    public class Pagamento
    {
        public Guid Id { get; set; }
        public Guid PedidoId { get; set; }
        public Pedido Pedido { get; set; } = null!;

        public decimal Valor { get; set; }
        public StatusPagamento Status { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}