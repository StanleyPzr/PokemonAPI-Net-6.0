﻿ using Pokemon.Models;

namespace Pokemon.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<PokemoN> GetPokemoNs();
        PokemoN GetPokemoN(int id);
        PokemoN GetPokemon(string name);
        decimal GetPokemonRating(int pokeid);
        bool PokemonExists(int pokeid);
        bool CreatePokemon(int entrenadorId, int categoriaId, PokemoN pokemon);
        bool UpdatePokemon(int entrenadorId, int categoriaId, PokemoN pokemon);
        bool DeletePokemon(PokemoN pokemon);
        bool Save();
    }
}
