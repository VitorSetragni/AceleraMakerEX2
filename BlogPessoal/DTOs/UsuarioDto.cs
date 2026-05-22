using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs{

    public class UsuarioDTO{

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Senha { get; set; } = string.Empty;

        
    }
}
