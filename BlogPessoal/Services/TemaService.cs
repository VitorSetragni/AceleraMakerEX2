using BlogPessoal.DTOs;
using BlogPessoal.Models;
using BlogPessoal.Repositories;

namespace BlogPessoal.Services{

    public class TemaService : ITemaService{

        private readonly ITemaRepository _temaRepository;

        public TemaService(ITemaRepository temaRepository){
            _temaRepository = temaRepository;
        }

        public async Task<List<Tema>> ListarTodosAsync(){
            return await _temaRepository.ListarTodosAsync();
        }

        public async Task<Tema?> BuscarPorIdAsync(long id){
            return await _temaRepository.BuscarPorIdAsync(id);
        }

        public async Task<Tema> CadastrarAsync(TemaDTO temaDTO){
            ValidarNome(temaDTO.Nome);

            Tema tema = new Tema(temaDTO.Nome);

            return await _temaRepository.CadastrarAsync(tema);
        }

        public async Task<Tema?> AtualizarAsync(long id, TemaDTO temaDTO){
            ValidarNome(temaDTO.Nome);

            Tema? tema = await _temaRepository.BuscarPorIdAsync(id);

            if(tema == null){
                return null;
            }

            tema.Nome = temaDTO.Nome;

            return await _temaRepository.AtualizarAsync(tema);
        }

        public async Task<bool> DeletarAsync(long id){
            return await _temaRepository.DeletarAsync(id);
        }

        private static void ValidarNome(string nome){
            if(string.IsNullOrWhiteSpace(nome)){
                throw new ArgumentException("O nome do tema é obrigatório.");
            }

            if(nome.Length < 3){
                throw new ArgumentException("O nome do tema deve ter pelo menos 3 caracteres.");
            }

            if(nome.Length > 100){
                throw new ArgumentException("O nome do tema deve ter no máximo 100 caracteres.");
            }
        }
    }
}