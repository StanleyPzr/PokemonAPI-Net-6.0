using Pokemon.Data;
using Pokemon.Interfaces;
using Pokemon.Models;

namespace Pokemon.Repository
{
    public class EntrenadorRepository : IEntrenadorRepository
    {
        private readonly DataContext _context;

        public EntrenadorRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateEntrenador(Entrenador entrenador)
        {
            _context.Add(entrenador);
            return Save();
        }

        public bool EntrenadorExist(int IdEntrenador)
        {
            return _context.Entrenador.Any(E => E.Id == IdEntrenador);
        }

        public Entrenador GetEntrenador(int IdEntrenador)
        {
            return _context.Entrenador.Where(E => E.Id == IdEntrenador).FirstOrDefault();
        }

        public ICollection<Entrenador> GetEntrenadores()
        {
            return _context.Entrenador.ToList();
        }

        public ICollection<Entrenador> GetEntrenadorPokemon(int pokeId)
        {
            return _context.EntrenadorPokemon.Where(p => p.PokemoN.Id == pokeId).Select(e => e.Entrenador).ToList();
        }

        public ICollection<PokemoN> GetPokemonEntrenador(int IdEntrenador)
        {
            return _context.EntrenadorPokemon.Where(E => E.Entrenador.Id == IdEntrenador).Select(p => p.PokemoN).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
