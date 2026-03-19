using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEcommerce.Models
{
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