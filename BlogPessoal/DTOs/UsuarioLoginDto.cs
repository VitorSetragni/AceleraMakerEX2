using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs{

    public class UsuarioLoginDTO{

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Senha { get; set; } = string.Empty;

        public string? Token { get; set; }
    }
}