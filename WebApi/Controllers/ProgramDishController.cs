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
using WebApi.Services.Interfaces;
using WebApi.Sorting;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramDishController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDishService _dishService;
        private readonly IProgramDayService _programDayService;
        public ProgramDishController(IDishService dishService, IMapper mapper, IProgramDayService programDayService)
        {
            _dishService = dishService;
            _mapper = mapper;
            _programDayService = programDayService;
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
           _dishService.AddNewProgramDish(dishDto);
            return Ok();
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
        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("listDishes")]
        public IActionResult Get()
        {
            var dishes = _dishService.GetProgramDishes();
            return Ok(dishes);
        }
    }
}
