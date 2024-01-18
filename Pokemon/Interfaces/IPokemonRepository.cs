 using Pokemon.Models;

namespace Pokemon.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<PokemoN> GetPokemoNs();
        PokemoN GetPokemoN(int id);
        PokemoN GetPokemon(string name);
        decimal GetPokemonRating(int pokeid);
        bool PokemonExists(int pokeid);
    }
}
