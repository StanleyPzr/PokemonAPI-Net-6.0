using Pokemon.Data;
using Pokemon.Interfaces;
using Pokemon.Models;

namespace Pokemon.Repository
{
    public class PokemonRepository : IPokemonRepository
    {

        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePokemon(int entrenadorId, int categoriaId, PokemoN pokemon) //Relacion muchos a muchos
        {
            var Entrenador = _context.Entrenador.Where(e => e.Id == entrenadorId).FirstOrDefault();
            var Categoria = _context.Categorias.Where(c => c.Id == categoriaId).FirstOrDefault();
            var EntrenadorPokemon = new EntrenadorPokemon()
            {
                Entrenador = Entrenador,
                PokemoN = pokemon
            };
            _context.Add(EntrenadorPokemon);

            var CategoriaPokemon = new CategoriaPokemon()
            {
                Categoria = Categoria,
                PokemoN = pokemon
            };
            _context.Add(CategoriaPokemon);

            _context.Add(pokemon);

            return Save();
        }

        public bool DeletePokemon(PokemoN pokemon)
        {
            _context.Remove(pokemon); 
            return Save();
        }

        public PokemoN GetPokemoN(int id)
        {
            return _context.PokemoN.Where(p => p.Id == id).FirstOrDefault(); 
        }

        public PokemoN GetPokemon(string name)
        {
            return _context.PokemoN.Where(p => p.Nombre == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeid)
        {
            var review = _context.Reseñas.Where(p => p.PokemoN.Id == pokeid);
            if (review.Count() <= 0)
                return 0;

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<PokemoN> GetPokemoNs()
        {
            return _context.PokemoN.OrderBy(P => P.Id).ToList();
        }

        public bool PokemonExists(int pokeid)
        {
            return _context.PokemoN.Any(p => p.Id == pokeid);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePokemon(int entrenadorId, int categoriaId, PokemoN pokemon)
        {
            _context.Update(pokemon);
            return Save();
        }
    }
}
