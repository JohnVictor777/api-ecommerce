using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEcommerce.Models
{
    public class ItemCarrinho
    {
        public Guid Id { get; set; }

        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;

        public Guid CarrinhoId { get; set; }
        public Carrinho Carrinho { get; set; } = null!;

        public int Quantidade { get; set; }
    }
}