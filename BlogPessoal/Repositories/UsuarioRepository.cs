using BlogPessoal.Data;
using BlogPessoal.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Repositories{

    public class UsuarioRepository : IUsuarioRepository{

        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context){
            _context = context;
        }

        public async Task<Usuario?> BuscarPorIdAsync(long id){
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario?> BuscarPorEmailAsync(string email){
            return await _context.Usuarios
                .FirstOrDefaultAsync(usuario => usuario.Email == email);
        }

        public async Task<Usuario> CadastrarAsync(Usuario usuario){
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario> AtualizarAsync(Usuario usuario){
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<bool> DeletarAsync(long id){
            Usuario? usuario = await BuscarPorIdAsync(id);

            if(usuario == null){
                return false;
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}