﻿using AutoMapper;
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
    }    
}