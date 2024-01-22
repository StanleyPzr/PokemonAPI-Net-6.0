using AutoMapper;
using Pokemon.Data;
using Pokemon.Interfaces;
using Pokemon.Models;

namespace Pokemon.Repository
{
    public class PaisRepository : IPaisRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PaisRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreatePais(Pais pais)
        {
            _context.Add(pais);
            return Save();
        }

        public ICollection<Entrenador> GetEntradorByPais(int IdPais)
        {
           return _context.Entrenador.Where(e => e.Pais.Id == IdPais).ToList();
        }

        public ICollection<Pais> GetPais()
        {
            return _context.Pais.ToList();
        }

        public Pais GetPais(int id)
        {
            return _context.Pais.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pais GetPaisByEntrenador(int IdEntrenador)
        {
            return _context.Entrenador.Where(e => e.Id == IdEntrenador).Select(p => p.Pais).FirstOrDefault();
        }

        public bool PaisExists(int Id)
        {
            return _context.Pais.Any(p => p.Id == Id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
