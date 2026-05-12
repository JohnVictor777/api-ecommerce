using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEcommerce.Features.Api.Produtos.DTOs.Update
{
    public class ProdutoUpdateDTO
    {
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
    }
}