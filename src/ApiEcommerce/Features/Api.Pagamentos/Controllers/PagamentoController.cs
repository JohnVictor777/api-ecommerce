using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiEcommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentoController : ControllerBase
    {
        private readonly ILogger<PagamentoController> _logger;

        public PagamentoController(ILogger<PagamentoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Requisição GET em Pagamento.");

            if (false) // Simulando um erro
            {
                throw new Exception("Erro de teste");
            }

            return Ok(new { Message = "Requisição GET em Usuario bem-sucedida." });
        }
    }
}