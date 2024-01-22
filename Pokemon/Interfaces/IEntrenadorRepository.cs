using Pokemon.Models;

namespace Pokemon.Interfaces
{
    public interface IEntrenadorRepository
    {

        ICollection<Entrenador> GetEntrenadores();
        Entrenador GetEntrenador(int IdEntrenador);
        ICollection<Entrenador> GetEntrenadorPokemon(int pokeId);
        ICollection<PokemoN> GetPokemonEntrenador(int IdEntrenador);
        bool EntrenadorExist(int IdEntrenador);
        bool UpdateEntrenador(Entrenador entrenador);
        bool CreateEntrenador(Entrenador entrenador);
        bool DeleteEntrenador(Entrenador entrenador);
        bool Save();

    }

}
