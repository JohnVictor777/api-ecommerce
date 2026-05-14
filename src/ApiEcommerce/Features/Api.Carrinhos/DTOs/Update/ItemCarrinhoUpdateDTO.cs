using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEcommerce.Features.Api.Carrinhos.DTOs.Update
{
    public class ItemCarrinhoUpdateDTO
    {
        [Required(ErrorMessage = "O ID do produto é obrigatório.")]
        public Guid ProdutoId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser no mínimo 1.")]
        public int Quantidade { get; set; }
    }
}