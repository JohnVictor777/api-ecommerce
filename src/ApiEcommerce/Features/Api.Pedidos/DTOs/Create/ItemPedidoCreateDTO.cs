using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEcommerce.DTOs
{
    public class ItemPedidoCreateDTO
    {
        [Required(ErrorMessage = "O ID do produto é obrigatório.")]
        public Guid ProdutoId { get; set; }

        [Range(1, 999, ErrorMessage = "A quantidade deve ser entre 1 e 999.")]
        public int Quantidade { get; set; }
    }
}