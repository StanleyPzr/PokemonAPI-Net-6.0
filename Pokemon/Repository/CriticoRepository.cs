using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pokemon.Data;
using Pokemon.Interfaces;
using Pokemon.Models;

namespace Pokemon.Repository
{
    public class CriticoRepository : ICriticoRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CriticoRepository(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateCritico(Critico critico)
        {
            _context.Add(critico);
            return Save();
        }

        public bool CriticoExists(int IdCritico)
        {
            return _context.Critico.Any(C => C.Id == IdCritico);
        }

        public Critico GetCritico(int IdCritico)
        {
            return _context.Critico.Where(C => C.Id == IdCritico).Include(r => r.Reseñas).FirstOrDefault();
        }

        public ICollection<Critico> GetCriticos()
        {
            return _context.Critico.ToList();
        }

        public ICollection<Reseña> GetReseñaCritico(int IdCritico)
        {
            return _context.Reseñas.Where(r => r.Critico.Id == IdCritico).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
