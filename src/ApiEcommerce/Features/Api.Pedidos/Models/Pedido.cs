using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiEcommerce.Models.Enum;

namespace ApiEcommerce.Models
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal Total { get; set; }

        public StatusPedido Status { get; set; }
        public List<ItemPedido> Itens { get; set; } = new();
        public Pagamento? Pagamento { get; set; }
    }
}