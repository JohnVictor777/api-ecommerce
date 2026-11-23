using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiEcommerce.Services;
using ApiEcommerce.DTOs;
using ApiEcommerce.Features.Api.Usuarios.DTOs.Update;
using System.Security.Claims;
using ApiEcommerce.Shared;
using Microsoft.AspNetCore.Authorization;
using ApiEcommerce.Features.Api.Usuarios.Services;

namespace ApiEcommerce.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarios = await _service.GetAll();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (!PodeAcessar(id))
                return Forbid();

            var usuario = await _service.GetById(id);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(UsuarioCreateDTO dto)
        {
            var usuario = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UsuarioUpdateDTO dto)
        {
            if (!PodeAcessar(id))
                return Forbid();

            await _service.Update(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!PodeAcessar(id))
                return Forbid();

            await _service.Delete(id);
            return NoContent();
        }

        // Um usuário só acessa o próprio cadastro; administradores acessam qualquer um.
        private bool PodeAcessar(Guid usuarioId)
        {
            return User.IsInRole(Roles.Admin)
                || User.FindFirstValue(ClaimTypes.NameIdentifier) == usuarioId.ToString();
        }
    }
}