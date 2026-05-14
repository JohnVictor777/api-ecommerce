using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiEcommerce.Features.Api.Usuarios.DTOs.Update
{
    public class UsuarioUpdateDTO
    {
                [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do usuário deve ter entre 3 e 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;
                [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public string Email { get; set; } = string.Empty;
                [StringLength(50, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 50 caracteres se informada.")]
        public string? Senha { get; set; } = string.Empty;
    }
}