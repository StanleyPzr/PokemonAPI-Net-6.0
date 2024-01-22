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
    public class EntrenadorController : ControllerBase
    {

        private readonly IEntrenadorRepository _entrenadorRepository;
        private readonly IPaisRepository _paisRepository;
        private readonly IMapper _mapper;

        public EntrenadorController(IEntrenadorRepository entrenadorRepository, IPaisRepository paisRepository, IMapper mapper)
        {
            _entrenadorRepository = entrenadorRepository;
            _paisRepository = paisRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Entrenador>))]
        public IActionResult GetEntrenadores() //Devuelve una lista.
        {
            var entrenadores = _mapper.Map<List<EntrenadorDto>>(_entrenadorRepository.GetEntrenadores());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(entrenadores);
        }

        [HttpGet("{entrenadorId}")]
        [ProducesResponseType(200, Type = typeof(Entrenador))]
        [ProducesResponseType(400)]
        public IActionResult GetEntrenador(int entrenadorId)
        {
            if (!_entrenadorRepository.EntrenadorExist(entrenadorId))
            {
                return NotFound();
            }
            var entrenador = _mapper.Map<EntrenadorDto>(_entrenadorRepository.GetEntrenador(entrenadorId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(entrenador);
        }

        [HttpGet("{IdEntrenador}/pokemon")]
        [ProducesResponseType(200, Type = typeof(Entrenador))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonEntrenador(int IdEntrenador) 
        { 
            if(!_entrenadorRepository.EntrenadorExist(IdEntrenador))
            {
                return NotFound();
            }

            var entrenador = _mapper.Map<List<PokemonDto>>(_entrenadorRepository.GetPokemonEntrenador(IdEntrenador));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(entrenador);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEntrenador([FromQuery] int IdPais, [FromBody] EntrenadorDto EntrenadorCreate)
        {
            if (EntrenadorCreate == null)
                return BadRequest(ModelState);

            var entrenadores = _entrenadorRepository.GetEntrenadores()
                .Where(c => c.Apellido.Trim().ToUpper() == EntrenadorCreate.Apellido.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (entrenadores != null)
            {
                ModelState.AddModelError("", "Entrenador ya existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var EntrenadorMap = _mapper.Map<Entrenador>(EntrenadorCreate);
            
            EntrenadorMap.Pais = _paisRepository.GetPais(IdPais);

            if (!_entrenadorRepository.CreateEntrenador(EntrenadorMap))
            {
                ModelState.AddModelError("", "Ocurrio un error mientras guardaba.");
                return StatusCode(500, ModelState);
            }

            return Ok("Creado Exitosamente");
        }


        [HttpPut("{entrenadorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdatePais(int entrenadorId, [FromBody] EntrenadorDto updatedEntrenador)
        {
            if (updatedEntrenador == null) return BadRequest(ModelState);

            if (entrenadorId != updatedEntrenador.Id) return BadRequest(ModelState);

            if (!_paisRepository.PaisExists(entrenadorId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest();

            var EntrenadorMap = _mapper.Map<Entrenador>(updatedEntrenador);

            if (!_entrenadorRepository.UpdateEntrenador(EntrenadorMap))
            {
                ModelState.AddModelError("", "Error al actualizar el entrenador");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{entrenadorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategoria(int entrenadorId)
        {
            if (!_entrenadorRepository.EntrenadorExist(entrenadorId))
            {
                return NotFound();
            }

            var entrenadorEliminar = _entrenadorRepository.GetEntrenador(entrenadorId);

            if (!ModelState.IsValid) return BadRequest();

            if (!_entrenadorRepository.DeleteEntrenador(entrenadorEliminar))
            {
                ModelState.AddModelError("", "Algo ha salido mal al eliminar el entrenador");
                return StatusCode(500, ModelState);
            }
            return Ok("Entrenador eliminada");
        }
    }
}
