using BlogPessoal.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.Data{

    public class AppDbContext : DbContext{

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Tema> Temas { get; set; }

        public DbSet<Postagem> Postagens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Postagem>()
                .HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(postagem => postagem.UsuarioId);

            modelBuilder.Entity<Postagem>()
                .HasOne<Tema>()
                .WithMany()
                .HasForeignKey(postagem => postagem.TemaId);
        }
    }
}