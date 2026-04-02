using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEcommerce.Models
{
    // Modelo para representar um item de pedido
    public class ItemPedido
    {
        public Guid Id { get; set; }

        public Guid ProdutoId { get; set; }
        public Produto? Produto { get; set; }

        public Guid PedidoId { get; set; }
        public Pedido? Pedido { get; set; }

        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

        public decimal CalcularValorTotal()
        {
            return Quantidade * PrecoUnitario;
        }
    }
}