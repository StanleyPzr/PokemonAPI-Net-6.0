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
    public class CriticoController : ControllerBase
    {
        private readonly ICriticoRepository _criticoRepository;
        private readonly IMapper _mapper;

        public CriticoController(ICriticoRepository criticoRepository, IMapper mapper)
        {
            _criticoRepository = criticoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Critico>))]
        public IActionResult GetCriticos()
        {

            var criticos = _mapper.Map<List<CriticoDto>>(_criticoRepository.GetCriticos());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(criticos);
        
        }

        [HttpGet("{criticoId}")]
        [ProducesResponseType(200, Type = typeof(Critico))]
        [ProducesResponseType(400)]
        public IActionResult GetCritico(int criticoId)
        {
            if (!_criticoRepository.CriticoExists(criticoId))
            {
                return NotFound();
            }

            var critico = _mapper.Map<CriticoDto>(_criticoRepository.GetCritico(criticoId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(critico);
        }

        [HttpGet("{criticoId}/Reseñas")]
        [ProducesResponseType(200, Type = typeof(Critico))]
        [ProducesResponseType(400)]
        public IActionResult GetReseñaCritico(int criticoId)
        {
            if (!_criticoRepository.CriticoExists(criticoId))
            {
                return NotFound();
            }
            var reseñas = _mapper.Map<List<ReseñaDto>>(_criticoRepository.GetReseñaCritico(criticoId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(reseñas);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCritico([FromBody] CriticoDto CriticoCreate)
        {
            if (CriticoCreate == null)
                return BadRequest(ModelState);

            var criticos = _criticoRepository.GetCriticos()
                .Where(c => c.Apellido.Trim().ToUpper() == CriticoCreate.Apellido.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (criticos != null)
            {
                ModelState.AddModelError("", "El critico ya existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var CriticoMap = _mapper.Map<Critico>(CriticoCreate);

            if (!_criticoRepository.CreateCritico(CriticoMap))
            {
                ModelState.AddModelError("", "Ocurrio un error mientras guardaba");
                return StatusCode(500, ModelState);
            }

            return Ok("Creado Exitosamente");
        }
    }
}
