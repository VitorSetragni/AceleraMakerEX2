using BlogPessoal.Data;
using BlogPessoal.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Repositories{

    public class PostagemRepository : IPostagemRepository{

        private readonly AppDbContext _context;

        public PostagemRepository(AppDbContext context){
            _context = context;
        }

        public async Task<List<Postagem>> ListarTodosAsync(){
            return await _context.Postagens.ToListAsync();
        }

        public async Task<Postagem?> BuscarPorIdAsync(long id){
            return await _context.Postagens.FindAsync(id);
        }

        public async Task<List<Postagem>> BuscarPorUsuarioIdAsync(long usuarioId){
            return await _context.Postagens
                .Where(postagem => postagem.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<List<Postagem>> BuscarPorTemaIdAsync(long temaId){
            return await _context.Postagens
                .Where(postagem => postagem.TemaId == temaId)
                .ToListAsync();
        }

        public async Task<List<Postagem>> FiltrarAsync(long? usuarioId, long? temaId){
            IQueryable<Postagem> consulta = _context.Postagens.AsQueryable();

            if(usuarioId.HasValue){
                consulta = consulta.Where(postagem => postagem.UsuarioId == usuarioId.Value);
            }

            if(temaId.HasValue){
                consulta = consulta.Where(postagem => postagem.TemaId == temaId.Value);
            }

            return await consulta.ToListAsync();
        }

        public async Task<Postagem> CadastrarAsync(Postagem postagem){
            await _context.Postagens.AddAsync(postagem);
            await _context.SaveChangesAsync();

            return postagem;
        }

        public async Task<Postagem> AtualizarAsync(Postagem postagem){
            _context.Postagens.Update(postagem);
            await _context.SaveChangesAsync();

            return postagem;
        }

        public async Task<bool> DeletarAsync(long id){
            Postagem? postagem = await BuscarPorIdAsync(id);

            if(postagem == null){
                return false;
            }

            _context.Postagens.Remove(postagem);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}