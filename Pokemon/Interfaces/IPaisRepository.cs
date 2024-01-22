using Pokemon.Models;

namespace Pokemon.Interfaces
{
    public interface IPaisRepository
    {
        ICollection<Pais> GetPais();
        Pais GetPais(int id);
        Pais GetPaisByEntrenador(int IdEntrenador);
        ICollection<Entrenador> GetEntradorByPais(int IdPais);
        bool PaisExists(int Id);
        bool CreatePais(Pais pais);
        bool UpdatePais(Pais pais);
        bool DeletePais(Pais pais);
        bool Save();
    }
}
