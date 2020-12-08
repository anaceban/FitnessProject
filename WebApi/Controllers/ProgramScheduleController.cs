using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationFitness.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Services;
using WebApi.Sorting;

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

        [HttpGet("getAll")]
        [Authorize(Roles = "admin")]
        public ActionResult<PagedCollectionResponse<ProgramScheduleDto>> Get([FromQuery] SampleFilterModel filter)
        {
            var programSchedules = _programScheduleService.GetProgramSchedules(filter);
            var result = PagedCollectionResponse<ProgramScheduleDto>.Create(programSchedules, filter, (p) => _mapper.Map<ProgramScheduleDto>(p));
            return result;
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "user")]
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
        [Authorize(Roles = "admin")]
        [HttpPost("create")]
        public IActionResult Post([FromBody] CreateProgramScheduleDto dto)
        {
            var program = _programScheduleService.AddNewProgramSchedule(dto);

            if (program == null)
                return BadRequest("Program with such name already exists");

            var result = _mapper.Map<CreateProgramScheduleDto>(program);

            return CreatedAtAction(nameof(Get), new { id = program.Id }, result);
        }
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] CreateProgramScheduleDto dto, int id)
        {
            var program = _programScheduleService.UpdateProgramSchedule(id, dto);

            if (program == null)
                return BadRequest("Another program with such FitnessProgramName already exists");

            return NoContent();
        }
        [AllowAnonymous]
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] UpdateProgramScheduleDto dto)
        {
            var program = _programScheduleService.PartialUpdate(id, dto);

            if (program == null)
                return NotFound();

            var result = _mapper.Map<UpdateProgramScheduleDto>(program);
            return Ok(result);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _programScheduleService.RemoveProgramScheduleById(id);
            return NoContent();
        }
        [HttpGet("get{typeId}")]
        [AllowAnonymous]
        public IActionResult GetProgramByType(int typeId)
        {
            var schedule = _programScheduleService.GetProgramByTypeId(typeId);
            if(schedule == null)
            {
                return BadRequest("Not found"); 
            }
            return Ok(schedule);
        }
    }
}
