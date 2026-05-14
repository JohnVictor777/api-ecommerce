using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ApiEcommerce.Models.Enum;

namespace ApiEcommerce.Features.Api.Pagamentos.DTOs.Update
{
    public class PagamentoUpdateDTO
    {
        [EnumDataType(typeof(StatusPagamento),
        ErrorMessage = "Status inválido")]
        public StatusPagamento Status { get; set; }
    }
}