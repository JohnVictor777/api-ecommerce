using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiEcommerce.DTOs
{
    public class CarrinhoCreateDTO
    {
        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "Pelo menos um item é obrigatório para criar um carrinho.")]
        [MinLength(1, ErrorMessage = "Pelo menos um item é obrigatório para criar um carrinho.")]
        public required List<ItemCarrinhoCreateDTO> Itens { get; set; }
    }
}