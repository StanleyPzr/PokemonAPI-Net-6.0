using AutoMapper;
using Pokemon.DTO;
using Pokemon.Models;

namespace Pokemon.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //GET's
            CreateMap<PokemoN, PokemonDto>();
            CreateMap<Categoria, CategoriaDto>();            
            CreateMap<Pais, PaisDto>();
            CreateMap<Entrenador, EntrenadorDto>();
            CreateMap<Reseña, ReseñaDto>();
            CreateMap<Critico, CriticoDto>();

            //POST's & PUT
            CreateMap<CategoriaDto, Categoria>();
            CreateMap<PaisDto, Pais>();
            CreateMap<EntrenadorDto, Entrenador>();
            CreateMap<PokemonDto, PokemoN>();
            CreateMap<ReseñaDto, Reseña>();
            CreateMap<CriticoDto, Critico>();
        }
    }
}
