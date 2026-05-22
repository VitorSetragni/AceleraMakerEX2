using BlogPessoal.DTOs;
using BlogPessoal.Models;

namespace BlogPessoal.Services{

    public interface IUsuarioService{

        Task<UsuarioRespostaDTO?> BuscarPorIdAsync(long id);

        Task<UsuarioRespostaDTO> CadastrarAsync(UsuarioDTO usuarioDTO);

        Task<UsuarioRespostaDTO?> AtualizarAsync(long id, UsuarioDTO usuarioDTO);

        Task<bool> DeletarAsync(long id);

        Task<UsuarioLogin?> LoginAsync(UsuarioLogin usuarioLogin);
    }
}