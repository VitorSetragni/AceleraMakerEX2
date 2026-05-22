using BlogPessoal.Models;

namespace BlogPessoal.Repositories{

    public interface IUsuarioRepository{

        Task<Usuario?> BuscarPorIdAsync(long id);

        Task<Usuario?> BuscarPorEmailAsync(string email);

        Task<Usuario> CadastrarAsync(Usuario usuario);

        Task<Usuario> AtualizarAsync(Usuario usuario);

        Task<bool> DeletarAsync(long id);
    }
}