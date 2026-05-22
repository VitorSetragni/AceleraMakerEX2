using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.DTOs{

    public class TemaDTO{

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Nome { get; set; } = string.Empty;
    }
}