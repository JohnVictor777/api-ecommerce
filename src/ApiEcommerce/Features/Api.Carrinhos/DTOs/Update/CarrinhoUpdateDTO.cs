using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.DTOs;

namespace ApiEcommerce.Features.Api.Carrinhos.DTOs.Update
{
    public class CarrinhoUpdateDTO
    {
        [Required(ErrorMessage = "A lista de itens é obrigatória.")]
        [MinLength(1, ErrorMessage = "Pelo menos um item é obrigatório.")]
        public required List<ItemCarrinhoUpdateDTO> Itens { get; set; }
    }
}