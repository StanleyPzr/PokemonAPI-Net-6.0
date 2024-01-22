using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pokemon.DTO;
using Pokemon.Interfaces;
using Pokemon.Models;
using Pokemon.Repository;

namespace Pokemon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IEntrenadorRepository _entrenadorRepository;
        private readonly IReseñaRepository _reseñaRepository;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepository pokemonRepository,
            IMapper mapper,
            ICategoriaRepository categoriaRepository,
            IEntrenadorRepository entrenadorRepository,
            IReseñaRepository reseñaRepository)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
            _categoriaRepository = categoriaRepository;
            _entrenadorRepository = entrenadorRepository;
            _reseñaRepository = reseñaRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PokemoN>))]
        public IActionResult GetPokemons() //Devuelve una lista.
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemoNs());

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemons);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(PokemoN))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
            {
                return NotFound();
            }
            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemoN(pokeId));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            return Ok(pokemon);
        }

        [HttpGet("{pokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
            {
                return NotFound();
            }
            
            var rating = _pokemonRepository.GetPokemonRating(pokeId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int IdEntrenador, [FromQuery] int IdCategoria, [FromBody] PokemonDto PokemonCreate)
        {
            if (PokemonCreate == null)
                return BadRequest(ModelState);

            var pokemones = _pokemonRepository.GetPokemoNs()
                .Where(p => p.Nombre.Trim().ToUpper() == PokemonCreate.Nombre.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (pokemones != null)
            {
                ModelState.AddModelError("", "El Pokemon ya existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var PokemonMap = _mapper.Map<PokemoN>(PokemonCreate);            

            if (!_pokemonRepository.CreatePokemon(IdEntrenador,IdCategoria,PokemonMap))
            {
                ModelState.AddModelError("", "Ocurrio un error mientras guardaba.");
                return StatusCode(500, ModelState);
            }

            return Ok("Creado Exitosamente");
        }


        [HttpPut("{pokeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePais(int pokeId, [FromQuery] int entrenadorId, [FromQuery] int categoriaId, [FromBody] PokemonDto updatedPokemon)
        {
            if (updatedPokemon == null) return BadRequest(ModelState);

            if (pokeId != updatedPokemon.Id) return BadRequest(ModelState);

            if (!_pokemonRepository.PokemonExists(pokeId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest();

            var PokemonMap = _mapper.Map<PokemoN>(updatedPokemon);

            if (!_pokemonRepository.UpdatePokemon(entrenadorId, categoriaId,PokemonMap))
            {
                ModelState.AddModelError("", "Error al actualizar el pokemon");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{pokeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePokemon(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
            {
                return NotFound();
            }

            var reseñaEliminar = _reseñaRepository.GetReseñasPokemon(pokeId);
            var pokemonEliminar = _pokemonRepository.GetPokemoN(pokeId);

            if (!ModelState.IsValid) return BadRequest();

            if (_reseñaRepository.DeleteReseñas(reseñaEliminar.ToList()))
            {
                ModelState.AddModelError("", "Algo ha salido mal al eliminar las reseñas");
                return StatusCode(500, ModelState);
            }

            if (!_pokemonRepository.DeletePokemon(pokemonEliminar))
            {
                ModelState.AddModelError("", "Algo ha salido mal al eliminar el pokemon");
                return StatusCode(500, ModelState);
            }
            return Ok("Pokemon eliminado");
        }

    }   

}
