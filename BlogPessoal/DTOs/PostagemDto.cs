using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs{

    public class PostagemDTO{

        [Required]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public string Texto { get; set; } = string.Empty;

        // O id do usario esta vindo direto do token quando o usario loga

        [Required]
        public long? TemaId { get; set; }
    }
}