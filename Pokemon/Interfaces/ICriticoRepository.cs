using Pokemon.Models;

namespace Pokemon.Interfaces
{
    public interface ICriticoRepository
    {
        ICollection<Critico> GetCriticos();
        Critico GetCritico(int IdCritico);
        ICollection<Reseña> GetReseñaCritico(int IdCritico);
        bool CriticoExists(int IdCritico);
        bool CreateCritico(Critico critico);
        bool Save();


    }
}
