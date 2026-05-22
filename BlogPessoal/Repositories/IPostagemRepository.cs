using BlogPessoal.Models;

namespace BlogPessoal.Repositories{

    public interface IPostagemRepository{

        Task<List<Postagem>> ListarTodosAsync();

        Task<Postagem?> BuscarPorIdAsync(long id);

        Task<List<Postagem>> BuscarPorUsuarioIdAsync(long usuarioId);

        Task<List<Postagem>> BuscarPorTemaIdAsync(long temaId);

        Task<List<Postagem>> FiltrarAsync(long? usuarioId, long? temaId);

        Task<Postagem> CadastrarAsync(Postagem postagem);

        Task<Postagem> AtualizarAsync(Postagem postagem);

        Task<bool> DeletarAsync(long id);
    }
}