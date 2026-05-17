using ApiEcommerce.Features.Api.Pagamentos.DTOs.Create;
using ApiEcommerce.Features.Api.Pagamentos.DTOs.Response;
using ApiEcommerce.Features.Api.Pagamentos.DTOs.Update;
using ApiEcommerce.Features.Api.Pagamentos.Services;
using ApiEcommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiEcommerce.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentoController : ControllerBase
    {
        private readonly PagamentoService _service;

        public PagamentoController(PagamentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<PagamentoResponseDTO>>> Get()
        {
            var pagamentos = await _service.GetAll();
            return Ok(pagamentos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PagamentoResponseDTO>> GetById(Guid id)
        {
            var pagamento = await _service.GetById(id);
            if (pagamento == null)
                return NotFound();

            return Ok(pagamento);
        }

        [HttpPost]
        public async Task<ActionResult<PagamentoResponseDTO>> Create([FromBody] PagamentoCreateDTO dto)
        {
            var created = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PagamentoUpdateDTO dto)
        {
            var updated = await _service.Update(id, dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.Delete(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}