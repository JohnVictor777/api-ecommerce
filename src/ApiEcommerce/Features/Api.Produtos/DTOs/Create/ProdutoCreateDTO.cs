using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiEcommerce.DTOs
{
        public class ProdutoCreateDTO
        {
                [Required(ErrorMessage = "O nome do produto é obrigatório.")]
                [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do produto deve ter entre 3 e 100 caracteres.")]
                public string Nome { get; set; } = string.Empty;

                [Range(0.01, double.MaxValue, ErrorMessage = "O preço do produto deve ser maior que zero.")]
                public decimal Preco { get; set; }

                [Range(0, int.MaxValue, ErrorMessage = "O estoque do produto não pode ser negativo.")]
                public int Estoque { get; set; }
        }
}