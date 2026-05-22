using BlogPessoal.DTOs;
using BlogPessoal.Models;
using BlogPessoal.Repositories;

namespace BlogPessoal.Services{

    public class PostagemService : IPostagemService{

        private readonly IPostagemRepository _postagemRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITemaRepository _temaRepository;

        public PostagemService(
            IPostagemRepository postagemRepository,
            IUsuarioRepository usuarioRepository,
            ITemaRepository temaRepository
        ){
            _postagemRepository = postagemRepository;
            _usuarioRepository = usuarioRepository;
            _temaRepository = temaRepository;
        }

        public async Task<List<Postagem>> ListarTodosAsync(){
            return await _postagemRepository.ListarTodosAsync();
        }

        public async Task<Postagem?> BuscarPorIdAsync(long id){
            return await _postagemRepository.BuscarPorIdAsync(id);
        }

        public async Task<List<Postagem>> BuscarPorUsuarioIdAsync(long usuarioId){
            Usuario? usuario = await _usuarioRepository.BuscarPorIdAsync(usuarioId);

            if(usuario == null){
                throw new ArgumentException("Usuário não encontrado.");
            }

            return await _postagemRepository.BuscarPorUsuarioIdAsync(usuarioId);
        }

        public async Task<List<Postagem>> BuscarPorTemaIdAsync(long temaId){
            Tema? tema = await _temaRepository.BuscarPorIdAsync(temaId);

            if(tema == null){
                throw new ArgumentException("Tema não encontrado.");
            }

            return await _postagemRepository.BuscarPorTemaIdAsync(temaId);
        }

        public async Task<List<Postagem>> FiltrarAsync(long? usuarioId, long? temaId){
            if(usuarioId.HasValue){
                Usuario? usuario = await _usuarioRepository.BuscarPorIdAsync(usuarioId.Value);

                if(usuario == null){
                    throw new ArgumentException("Usuário não encontrado.");
                }
            }

            if(temaId.HasValue){
                Tema? temaEncontrado = await _temaRepository.BuscarPorIdAsync(temaId.Value);

                if(temaEncontrado == null){
                    throw new ArgumentException("Tema não encontrado.");
                }
            }

            return await _postagemRepository.FiltrarAsync(usuarioId, temaId);
        }

        public async Task<Postagem> CadastrarAsync(PostagemDTO postagemDTO, long usuarioId){
            await ValidarPostagemAsync(postagemDTO, usuarioId);

            Postagem postagem = new Postagem(
                postagemDTO.Titulo,
                postagemDTO.Texto,
                usuarioId,
                postagemDTO.TemaId.Value
            );

            return await _postagemRepository.CadastrarAsync(postagem);
        }

        public async Task<Postagem?> AtualizarAsync(long id, PostagemDTO postagemDTO, long usuarioId){
            await ValidarPostagemAsync(postagemDTO, usuarioId);

            Postagem? postagem = await _postagemRepository.BuscarPorIdAsync(id);

            if(postagem == null){
                return null;
            }

            if(postagem.UsuarioId != usuarioId){
                throw new UnauthorizedAccessException("Você não pode editar uma postagem de outro usuário.");
            }

            postagem.Titulo = postagemDTO.Titulo;
            postagem.Texto = postagemDTO.Texto;
            postagem.TemaId = postagemDTO.TemaId.Value;

            return await _postagemRepository.AtualizarAsync(postagem);
        }

       public async Task<bool> DeletarAsync(long id, long usuarioId){
            Postagem? postagem = await _postagemRepository.BuscarPorIdAsync(id);

            if(postagem == null){
                return false;
            }

            if(postagem.UsuarioId != usuarioId){
                throw new UnauthorizedAccessException("Você não pode deletar uma postagem de outro usuário.");
            }

            return await _postagemRepository.DeletarAsync(id);
        }

        private async Task ValidarPostagemAsync(PostagemDTO postagemDTO, long usuarioId){
            if(string.IsNullOrWhiteSpace(postagemDTO.Titulo)){
                throw new ArgumentException("O título da postagem é obrigatório.");
            }

            if(string.IsNullOrWhiteSpace(postagemDTO.Texto)){
                throw new ArgumentException("O texto da postagem é obrigatório.");
            }

            Usuario? usuario = await _usuarioRepository.BuscarPorIdAsync(usuarioId);

            if(usuario == null){
                throw new ArgumentException("Usuário não encontrado.");
            }

            Tema? tema = await _temaRepository.BuscarPorIdAsync(postagemDTO.TemaId.Value);

            if(tema == null){
                throw new ArgumentException("Tema não encontrado.");
            }

            if(postagemDTO.TemaId == null || postagemDTO.TemaId <= 0){
                throw new ArgumentException("O tema da postagem é obrigatório.");
            }
        }
    }
}