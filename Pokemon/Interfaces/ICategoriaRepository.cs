using Pokemon.Models;

namespace Pokemon.Interfaces
{
    public interface ICategoriaRepository
    {
        ICollection<Categoria> GetCategorias();
        Categoria GetCategoria(int id);
        ICollection<PokemoN> GetPokemonByCategoria(int IdCategoria);
        bool CategoryExists(int id);
        bool CreateCategoria(Categoria categoria);
        bool UpdateCategoria(Categoria categoria);
        bool DeleteCategoria(Categoria categoria);
        bool Save();
    }
}
