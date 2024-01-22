using AutoMapper;
using Pokemon.Data;
using Pokemon.DTO;
using Pokemon.Interfaces;
using Pokemon.Models;

namespace Pokemon.Repository
{
    public class ReseñaRepository : IReseñaRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReseñaRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateReseña(Reseña reseña)
        {
            _context.Add(reseña);
            return Save();
        }

        public Reseña GetReseña(int IdReseña)
        {
            return _context.Reseñas.Where(r => r.Id == IdReseña).FirstOrDefault();
        }

        public ICollection<Reseña> GetReseñas()
        {
            return _context.Reseñas.ToList();
        }

        public ICollection<Reseña> GetReseñasPokemon(int pokeId)
        {
            return _context.Reseñas.Where(r => r.PokemoN.Id == pokeId).ToList();
        }

        public bool ReseñaExists(int IdReseña)
        {
            return _context.Reseñas.Any(r => r.Id == IdReseña);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
