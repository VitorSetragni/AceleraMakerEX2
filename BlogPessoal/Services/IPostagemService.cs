using BlogPessoal.DTOs;
using BlogPessoal.Models;

namespace BlogPessoal.Services{

    public interface IPostagemService{

        Task<List<Postagem>> ListarTodosAsync();

        Task<Postagem?> BuscarPorIdAsync(long id);

        Task<List<Postagem>> BuscarPorUsuarioIdAsync(long usuarioId);

        Task<List<Postagem>> BuscarPorTemaIdAsync(long temaId);

        Task<List<Postagem>> FiltrarAsync(long? usuarioId, long? temaId);

        Task<Postagem> CadastrarAsync(PostagemDTO postagemDTO, long usuarioId);

        Task<Postagem?> AtualizarAsync(long id, PostagemDTO postagemDTO, long usuarioId);

        Task<bool> DeletarAsync(long id, long usuarioId);
    }
}