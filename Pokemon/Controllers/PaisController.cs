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
    public class PaisController : ControllerBase
    {
        private readonly IPaisRepository _paisRepository;
        private readonly IMapper _mapper;

        public PaisController(IPaisRepository paisRepository, IMapper mapper)
        {
            _paisRepository = paisRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pais>))]
        public IActionResult GetPais() //Devuelve una lista.
        {
            var pais = _mapper.Map<List<PaisDto>>(_paisRepository.GetPais());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pais);
        }

        [HttpGet("{paisId}")]
        [ProducesResponseType(200, Type = typeof(Pais))]
        [ProducesResponseType(400)]
        public IActionResult GetPais(int paisId)
        {
            if (!_paisRepository.PaisExists(paisId))
            {
                return NotFound();
            }
            var pais = _mapper.Map<PaisDto>(_paisRepository.GetPais(paisId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pais);
        }

        [HttpGet("/Entrenador/{IdEntrenador}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Pais))]
        public IActionResult GetPaisByEntrenador(int IdEntrenador)
        {
             var pais = _mapper.Map<PaisDto>(_paisRepository.GetPaisByEntrenador(IdEntrenador));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pais);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePais([FromBody] PaisDto PaisCreate)
        {
            if (PaisCreate == null)
                return BadRequest(ModelState);

            var pais = _paisRepository.GetPais()
                .Where(c => c.Nombre.Trim().ToUpper() == PaisCreate.Nombre.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (pais != null)
            {
                ModelState.AddModelError("", "Pais ya existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var PaisMap = _mapper.Map<Pais>(PaisCreate);

            if (!_paisRepository.CreatePais(PaisMap))
            {
                ModelState.AddModelError("", "Ocurrio un error mientras guardaba");
                return StatusCode(500, ModelState);
            }

            return Ok("Creado Exitosamente");
        }
    }
}
