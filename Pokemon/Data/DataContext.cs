using Microsoft.EntityFrameworkCore;
using Pokemon.Models;


namespace Pokemon.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Entrenador>Entrenador { get; set;}
        public DbSet<PokemoN> PokemoN { get; set; }
        public DbSet<EntrenadorPokemon> EntrenadorPokemon {  get; set; }
        public DbSet<CategoriaPokemon> CategoriaPokemon { get; set; }
        public DbSet<Reseña> Reseñas { get; set; }
        public DbSet<Critico> Critico { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaPokemon>().HasKey(pc => new { pc.IdPokemon, pc.IdCategoria });
            modelBuilder.Entity<CategoriaPokemon>().HasOne(p => p.PokemoN).WithMany(pc => pc.CategoriaPokemon).HasForeignKey(c => c.IdPokemon);
            modelBuilder.Entity<CategoriaPokemon>().HasOne(p => p.Categoria).WithMany(pc => pc.CategoriaPokemon).HasForeignKey(c => c.IdCategoria);

            modelBuilder.Entity<EntrenadorPokemon>().HasKey(pc => new { pc.IdPokemon, pc.IdEntrenador });
            modelBuilder.Entity<EntrenadorPokemon>().HasOne(p => p.PokemoN).WithMany(pc => pc.EntrenadorPokemon).HasForeignKey(c => c.IdPokemon);
            modelBuilder.Entity<EntrenadorPokemon>().HasOne(p => p.Entrenador).WithMany(pc => pc.EntrenadorPokemon).HasForeignKey(c => c.IdEntrenador);
        }


    }
}
