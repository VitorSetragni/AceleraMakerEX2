using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.Models
{
    public class Usuario
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Senha { get; set; } = string.Empty;

        public string? Foto { get; set; }

        // Construtor vazio utilizado pelo Entity Framework.
        public Usuario()
        {
            
        }

        public Usuario(string nome, string email, string senha, string? foto)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Foto = foto;
            
        }
    }
}