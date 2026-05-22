using BlogPessoal.DTOs;
using BlogPessoal.Models;

namespace BlogPessoal.Services{

    public interface ITemaService{

        Task<List<Tema>> ListarTodosAsync();

        Task<Tema?> BuscarPorIdAsync(long id);

        Task<Tema> CadastrarAsync(TemaDTO temaDTO);

        Task<Tema?> AtualizarAsync(long id, TemaDTO temaDTO);

        Task<bool> DeletarAsync(long id);
    }
}