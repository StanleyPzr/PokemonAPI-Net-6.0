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
    }
}
