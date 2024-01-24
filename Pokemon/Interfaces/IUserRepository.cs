using Pokemon.Models;
using Pokemon.Models.Auth;

namespace Pokemon.Interfaces
{
    public interface IUserRepository
    {        
        bool CreateUsuario(Usuario usuario);
        Usuario GetByEmail(string email);
        Usuario GetById(int id);
        bool Save();
    }
}
