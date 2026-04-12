using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEcommerce.DTOs
{
    public class CarrinhoCreateDTO
    {
        public Guid UsuarioId { get; set; }
        public required List<ItemCarrinhoCreateDTO> Itens { get; set; }
    }
}