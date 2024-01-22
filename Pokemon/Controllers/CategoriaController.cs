using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Data;
using Pokemon.DTO;
using Pokemon.Interfaces;
using Pokemon.Models;
using Pokemon.Repository;


namespace Pokemon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriaRepository categoriaRepository, IMapper mapper) 
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Categoria>))]
        public IActionResult GetCategoria() //Devuelve una lista.
        {
            var categorias = _mapper.Map<List<CategoriaDto>>(_categoriaRepository.GetCategorias());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categorias);
        }

        [HttpGet("{categoriaId}")]
        [ProducesResponseType(200, Type = typeof(Categoria))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int categoriaId)
        {
            if (!_categoriaRepository.CategoryExists(categoriaId))
            {
                return NotFound();
            }
            var categoria = _mapper.Map<CategoriaDto>(_categoriaRepository.GetCategoria(categoriaId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categoria);
        }

        [HttpGet("pokemon/{Idcategoria}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PokemoN>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByCategory(int Idcategoria)
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_categoriaRepository.GetPokemonByCategoria(Idcategoria));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemons);

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoriaDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

            var category = _categoriaRepository.GetCategorias()
                .Where(c => c.Nombre.Trim().ToUpper() == categoryCreate.Nombre.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Categoria>(categoryCreate);

            if (!_categoriaRepository.CreateCategoria(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{categoriaId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategoria(int categoriaId, [FromBody] CategoriaDto updatedCategoria)
        {
            if (updatedCategoria == null) return BadRequest(ModelState);

            if (categoriaId != updatedCategoria.Id) return BadRequest(ModelState);

            if (!_categoriaRepository.CategoryExists(categoriaId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest();

            var categoriaMap = _mapper.Map<Categoria>(updatedCategoria);

            if (!_categoriaRepository.UpdateCategoria(categoriaMap))
            {
                ModelState.AddModelError("", "Error al actualizar la categoria");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{categoriaId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategoria(int categoriaId)
        {
            if (!_categoriaRepository.CategoryExists(categoriaId))
            {
                return NotFound();
            }

            var categoriaEliminar = _categoriaRepository.GetCategoria(categoriaId);

            if(!ModelState.IsValid) return BadRequest();

            if (!_categoriaRepository.DeleteCategoria(categoriaEliminar))
            {
                ModelState.AddModelError("", "aLGO HA SALIDO MAL AL ELIMINAR LA CATEGORIA");
                return StatusCode(500, ModelState);
            }
            return Ok("Categoria eliminada");
        }
    }    
}
