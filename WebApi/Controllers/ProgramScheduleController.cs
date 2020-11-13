using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationFitness.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramScheduleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProgramScheduleService _programScheduleService;

        public ProgramScheduleController(IProgramScheduleService programScheduleService, IMapper mapper)
        {
            _programScheduleService = programScheduleService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var programSchedules = _programScheduleService.GetProgramSchedules();
            var rezult = programSchedules.Select(p => _mapper.Map<ProgramScheduleDto>(p));
            return Ok(rezult);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var programsSchedule = _programScheduleService.GetProgramScheduleById(id);
            if (programsSchedule == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_mapper.Map<ProgramScheduleDto>(programsSchedule));
            }
        }
        [HttpGet("{user}")]
        public IActionResult GetScheduleForUser(User user)
        {
            var programSchedule = _programScheduleService.FindProgramForUser(user);

            return Ok(_mapper.Map<ProgramScheduleDto>(programSchedule));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateProgramScheduleDto dto)
        {
            var program = _programScheduleService.AddNewProgramSchedule(dto);

            if (program == null)
                return BadRequest("Program with such name already exists");

            var result = _mapper.Map<ProgramScheduleDto>(program);

            return CreatedAtAction(nameof(Get), new { id = program.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CreateProgramScheduleDto dto)
        {
            var program = _programScheduleService.UpdateProgramSchedule(id, dto);

            if (program == null)
                return BadRequest("Another program with such FitnessProgramName already exists");

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] UpdateProgramScheduleDto dto)
        {
            var program = _programScheduleService.PartialUpdate(id, dto);

            if (program == null)
                return NotFound();

            var result = _mapper.Map<ProgramScheduleDto>(program);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _programScheduleService.RemoveProgramScheduleById(id);
            return NoContent();
        }
    }
}
