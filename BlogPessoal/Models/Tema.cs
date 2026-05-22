using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.Models
{
    public class Tema
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Nome { get; set; }

        // Construtor vazio utilizado pelo Entity Framework.
        public Tema(){
            
        }

        public Tema(string nome){
            Nome = nome;
        }
    }
}