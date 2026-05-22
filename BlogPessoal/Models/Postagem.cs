using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.Models
{
    public class Postagem
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public string Texto { get; set; } = string.Empty;

        public DateTime Data { get; set; }

        [Required]
        public long UsuarioId { get; set; }

        [Required]
        public long TemaId { get; set; }

        // Construtor vazio utilizado pelo Entity Framework.
        public Postagem()
        {
            Data = DateTime.UtcNow;
        }

        public Postagem(string titulo, string texto, long usuarioId, long temaId)
        {
            Titulo = titulo;
            Texto = texto;
            UsuarioId = usuarioId;
            TemaId = temaId;
            Data = DateTime.UtcNow;
        }
    }
}