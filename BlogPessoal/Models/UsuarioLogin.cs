using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.Models
{
    public class UsuarioLogin
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Senha { get; set; } = string.Empty;

        public string? Token { get; set; }

        // Construtor vazio utilizado pelo Entity Framework.
        public UsuarioLogin()
        {
        }

        public UsuarioLogin(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }
    }
}