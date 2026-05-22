using BlogPessoal.Data;
using BlogPessoal.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Repositories{

    public class TemaRepository : ITemaRepository{

        private readonly AppDbContext _context;

        public TemaRepository(AppDbContext context){
            _context = context;
        }

        public async Task<List<Tema>> ListarTodosAsync(){
            return await _context.Temas.ToListAsync();
        }

        public async Task<Tema?> BuscarPorIdAsync(long id){
            return await _context.Temas.FindAsync(id);
        }

        public async Task<Tema> CadastrarAsync(Tema tema){
            await _context.Temas.AddAsync(tema);
            await _context.SaveChangesAsync();

            return tema;
        }

        public async Task<Tema> AtualizarAsync(Tema tema){
            _context.Temas.Update(tema);
            await _context.SaveChangesAsync();

            return tema;
        }

        public async Task<bool> DeletarAsync(long id){
            Tema? tema = await BuscarPorIdAsync(id);

            if(tema == null){
                return false;
            }

            _context.Temas.Remove(tema);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}