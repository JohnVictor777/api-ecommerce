using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApiEcommerce.Data
{
    public class ConnectionFactory : DbContext
    {
        public ConnectionFactory(DbContextOptions<ConnectionFactory> options) : base(options)
        {
        }

        // Defina os DbSet para cada entidade do seu modelo
        public DbSet<Models.Usuario> Usuarios { get; set; }
        public DbSet<Models.Produto> Produtos { get; set; }
        public DbSet<Models.Pedido> Pedidos { get; set; }
        public DbSet<Models.ItemPedido> ItensPedido { get; set; }
        public DbSet<Models.Carrinho> Carrinhos { get; set; }
        public DbSet<Models.ItemCarrinho> ItensCarrinho { get; set; }
        public DbSet<Models.Pagamento> Pagamentos { get; set; }

    }
}