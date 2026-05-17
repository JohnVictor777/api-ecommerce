using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEcommerce.Data
{
    public class ConnectionFactory : DbContext
    {
        public ConnectionFactory(DbContextOptions<ConnectionFactory> options) : base(options)
        {
        }

        // Define os DbSet para cada entidade do seu modelo
        public DbSet<Models.Usuario> Usuarios { get; set; }
        public DbSet<Models.Produto> Produtos { get; set; }
        public DbSet<Models.Pedido> Pedidos { get; set; }
        public DbSet<Models.ItemPedido> ItensPedido { get; set; }
        public DbSet<Models.Carrinho> Carrinhos { get; set; }
        public DbSet<Models.ItemCarrinho> ItensCarrinho { get; set; }
        public DbSet<Models.Pagamento> Pagamentos { get; set; }

        // Configurações adicionais do modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>()
                .Property(p => p.Preco)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Pedido>()
                .Property(p => p.Total)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Pagamento>()
                .Property(p => p.Valor)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ItemCarrinho>()
                .Property(i => i.PrecoUnitario)
                .HasPrecision(18, 2);
        }

    }
}