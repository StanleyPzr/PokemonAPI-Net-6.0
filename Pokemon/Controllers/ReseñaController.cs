using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokemon.DTO;
using Pokemon.Interfaces;
using Pokemon.Models;
using Pokemon.Repository;

namespace Pokemon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReseñaController : ControllerBase
    {

        private readonly IReseñaRepository _reseñaRepository;
        private readonly ICriticoRepository _criticoRepository;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public ReseñaController(IReseñaRepository reseñaRepository, ICriticoRepository criticoRepository,  IMapper mapper, IPokemonRepository pokemonRepository)
        {
            _reseñaRepository = reseñaRepository;
            _criticoRepository = criticoRepository;
            _mapper = mapper;
            _pokemonRepository = pokemonRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reseña>))]
        public IActionResult GetReseñas() //Devuelve una lista.
        {
            var reseñas = _mapper.Map<List<ReseñaDto>>(_reseñaRepository.GetReseñas());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reseñas);
        }

        [HttpGet("{reseñaId}")]
        [ProducesResponseType(200, Type = typeof(Reseña))]
        [ProducesResponseType(400)]
        public IActionResult GetReseña(int reseñaId)
        {
            if (!_reseñaRepository.ReseñaExists(reseñaId))
            {
                return NotFound();
            }
            var reseña = _mapper.Map<ReseñaDto>(_reseñaRepository.GetReseña(reseñaId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reseña);
        }

        [HttpGet("Pokemon/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Reseña))]
        [ProducesResponseType(400)]
        public IActionResult GetReseñasPokemon(int pokeId)
        {
            var reseña = _mapper.Map<List<ReseñaDto>>(_reseñaRepository.GetReseñasPokemon(pokeId));

            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            return Ok(reseña);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReseña([FromQuery] int IdCritico, [FromQuery] int IdPokemon, [FromBody] ReseñaDto ReseñaCreate)
        {
            if (ReseñaCreate == null)
                return BadRequest(ModelState);

            var reseñas = _reseñaRepository.GetReseñas()
                .Where(r =>r.Titulo.Trim().ToUpper() == ReseñaCreate.Titulo.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (reseñas != null)
            {
                ModelState.AddModelError("", "La Reseñea ya existe con ese titulo");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ReseñaMap = _mapper.Map<Reseña>(ReseñaCreate);

            ReseñaMap.Critico = _criticoRepository.GetCritico(IdCritico);
            ReseñaMap.PokemoN = _pokemonRepository.GetPokemoN(IdPokemon);

            if (!_reseñaRepository.CreateReseña(ReseñaMap))
            {
                ModelState.AddModelError("", "Ocurrio un error mientras guardaba.");
                return StatusCode(500, ModelState);
            }

            return Ok("Creado Exitosamente");
        }

        [HttpPut("{reseñaId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReseña(int reseñaId, [FromBody] ReseñaDto updatedReseña)
        {
            if (updatedReseña == null) return BadRequest(ModelState);

            if (reseñaId != updatedReseña.Id) return BadRequest(ModelState);

            if (!_reseñaRepository.ReseñaExists(reseñaId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest();

            var ReseñaMap = _mapper.Map<Reseña>(updatedReseña);

            if (!_reseñaRepository.UpdateReseña(ReseñaMap))
            {
                ModelState.AddModelError("", "Error al actualizar la reseña");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{reseñaId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReseña(int reseñaId)
        {
            if (!_reseñaRepository.ReseñaExists(reseñaId))
            {
                return NotFound();
            }

            var reseñaEliminar = _reseñaRepository.GetReseña(reseñaId);

            if (!ModelState.IsValid) return BadRequest();

            if (!_reseñaRepository.DeleteReseña(reseñaEliminar))
            {
                ModelState.AddModelError("", "Algo ha salido mal al eliminar la reseña");
                return StatusCode(500, ModelState);
            }
            return Ok("Reseña eliminada");
        }

        [HttpDelete("DeleteReseñaCritico/{criticoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReviewsByReviewer(int criticoId)
        {
            if (!_criticoRepository.CriticoExists(criticoId))
                return NotFound();

            var reviewsToDelete = _criticoRepository.GetReseñaCritico(criticoId).ToList();
            if (!ModelState.IsValid)
                return BadRequest();

            if (!_reseñaRepository.DeleteReseñas(reviewsToDelete))
            {
                ModelState.AddModelError("", "Error al eliminar las reseñas");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
