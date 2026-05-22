using BlogPessoal.Models;

namespace BlogPessoal.Repositories{

    public interface ITemaRepository{

        Task<List<Tema>> ListarTodosAsync();

        Task<Tema?> BuscarPorIdAsync(long id);

        Task<Tema> CadastrarAsync(Tema tema);

        Task<Tema> AtualizarAsync(Tema tema);

        Task<bool> DeletarAsync(long id);
    }
}