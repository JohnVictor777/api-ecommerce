using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEcommerce.DTOs
{
    public class ItemPedidoCreateDTO
    {
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}