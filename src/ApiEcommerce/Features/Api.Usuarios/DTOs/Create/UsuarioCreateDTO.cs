using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ApiEcommerce.DTOs
{
    public class UsuarioCreateDTO
    {
        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do usuário deve ter entre 3 e 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail do usuário é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 50 caracteres.")]
        public string Senha { get; set; } = string.Empty;

    }
}