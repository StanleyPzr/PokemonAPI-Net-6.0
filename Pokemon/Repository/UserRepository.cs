using Microsoft.EntityFrameworkCore.Diagnostics;
using Pokemon.Data;
using Pokemon.Interfaces;
using Pokemon.Models.Auth;

namespace Pokemon.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context) 
        {
            _context = context;
        }       

        public bool CreateUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            return Save();
        }

        public Usuario GetByEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public Usuario GetById(int id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
