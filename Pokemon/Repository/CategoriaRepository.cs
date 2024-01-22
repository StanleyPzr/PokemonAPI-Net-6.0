using Pokemon.Data;
using Pokemon.Interfaces;
using Pokemon.Models;

namespace Pokemon.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private DataContext _context;
        public CategoriaRepository(DataContext context)
        {
            _context = context;         
        }
        public bool CategoryExists(int id)
        {
            return _context.Categorias.Any(c => c.Id == id);
        }

        public bool CreateCategoria(Categoria categoria)
        {
            _context.Add(categoria);            
            return Save();
        }

        public bool DeleteCategoria(Categoria categoria)
        {
            _context.Remove(categoria);
            return Save();
        }

        public Categoria GetCategoria(int id)
        {
            return _context.Categorias.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Categoria> GetCategorias()
        {
            return _context.Categorias.ToList();
        }

        public ICollection<PokemoN> GetPokemonByCategoria(int IdCategoria)
        {
            return _context.CategoriaPokemon.Where(c => c.IdCategoria == IdCategoria).Select(c => c.PokemoN).ToList(); // se puede usar Include en vez de slect si es que se necesitan mas propiedades de PokemoN.
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategoria(Categoria categoria)
        {
           _context.Update(categoria);
           return Save();
        }
    }
}
