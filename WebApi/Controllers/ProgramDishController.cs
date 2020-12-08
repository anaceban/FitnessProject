using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Services;
using WebApi.Sorting;

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
        [Authorize(Roles ="admin")]
        [HttpGet]
        [Route("getAll")]
        public PagedCollectionResponse<DishDto> Get([FromQuery] SampleFilterModel filter)
        {
            var dishes = _dishService.GetProgramDishes(filter.Term);
            var result = PagedCollectionResponse<DishDto>.Create(dishes, filter, (d) => _mapper.Map<DishDto>(d));
            return result;
        }
        [AllowAnonymous]
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

        [Authorize(Roles ="admin")]
        [HttpPost]
        [Route("add")]
        public IActionResult Post([FromBody] DishDto dishDto)
        {
            var dish = _dishService.GetProgramDishes().Find(d => d.Name == dishDto.Name && d.TypeOfMeal == dishDto.TypeOfMeal);
            if(dish == null)
            {
                _dishService.AddNewProgramDish(dishDto);
            }
            
            if (dish != null)
                return BadRequest("Dish with such name already exists");

            var result = _mapper.Map<DishDto>(dish);

            return Ok(result);
        }
        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("{id}")]
        public IActionResult Put([FromBody] DishDto dishDto, int id)
        {
            var dish = _dishService.UpdateProgramDish(dishDto, id);
            if (dish == null)
                return BadRequest();

            return NoContent();

        }

        [Authorize(Roles= "admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _dishService.RemoveProgramDishById(id);
            return NoContent();
        }
    }
}
