using Pokemon.Models;

namespace Pokemon.Interfaces
{
    public interface IReseñaRepository
    {
        ICollection<Reseña> GetReseñas();
        Reseña GetReseña(int IdReseña);
        ICollection<Reseña> GetReseñasPokemon(int pokeId);
        bool ReseñaExists(int IdReseña);
        bool CreateReseña(Reseña reseña);
        bool UpdateReseña(Reseña reseña);
        bool DeleteReseña(Reseña reseña);
        bool DeleteReseñas(List<Reseña> reseñas);
        bool Save();
    }
}
