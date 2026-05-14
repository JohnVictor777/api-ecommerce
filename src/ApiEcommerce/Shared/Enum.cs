using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEcommerce.Models
{
    // Enum para representar os status dos pedidos e pagamentos
    public class Enum
    {
        public enum StatusPedido
        {
            Pendente = 1,
            Processando = 2,
            Enviado = 3,
            Entregue = 4,
            Cancelado = 5
        }

        public enum StatusPagamento
        {
            Pendente = 1,
            Aprovado = 2,
            Recusado = 3,
            Estornado = 4
        }

    }
}