using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramDishController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDishService _dishService;

        public ProgramDishController(IDishService dishService, IMapper mapper)
        {
            _dishService = dishService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var dishes = _dishService.GetProgramDishes();
            var result = dishes.Select(p => _mapper.Map<DishDto>(p)).ToList();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var dish = _dishService.GetProgramDishById(id);
            if (dish == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_mapper.Map<DishDto>(dish));
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] DishDto dishDto)
        {
            var dish = _dishService.AddNewProgramDish(dishDto);

            if (dish == null)
                return BadRequest("Dish with such name already exists");

            var result = _mapper.Map<DishDto>(dish);

            return CreatedAtAction(nameof(Get), new { id = dish.Id }, result);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _dishService.RemoveProgramDishById(id);
            return NoContent();
        }
    }
}
