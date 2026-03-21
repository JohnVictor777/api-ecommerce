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
            Pendente,
            Processando,
            Enviado,
            Entregue,
            Cancelado
        }

        public enum StatusPagamento
        {
            Pendente,
            Aprovado,
            Recusado,
            Estornado
        }

    }
}