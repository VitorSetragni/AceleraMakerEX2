namespace BlogPessoal.DTOs{
// Para facilitar na hora de devolver não vir o campo da senha para não ter a chance de vazar as senhas
    public class UsuarioRespostaDTO{

        public long Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;


        public UsuarioRespostaDTO(){
        }

        public UsuarioRespostaDTO(long id, string nome, string email){
            Id = id;
            Nome = nome;
            Email = email;
        }
    }
}
