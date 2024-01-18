using AutoMapper;
using Pokemon.DTO;
using Pokemon.Models;

namespace Pokemon.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<PokemoN, PokemonDto>();
            CreateMap<Categoria, CategoriaDto>();
            CreateMap<Pais, PaisDto>();
        }
    }
}
