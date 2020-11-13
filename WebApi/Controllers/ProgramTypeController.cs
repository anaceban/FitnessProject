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
    public class ProgramTypeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProgramTypeService _programTypeService;

        public ProgramTypeController(IProgramTypeService programTypeService, IMapper mapper)
        {
            _programTypeService = programTypeService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var programTypes = _programTypeService.GetProgramTypes();
            var rezult = programTypes.Select(p => _mapper.Map<ProgramTypeDto>(p)).ToList();
            return Ok(rezult);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var programType = _programTypeService.GetProgramById(id);
            if (programType == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_mapper.Map<ProgramTypeDto>(programType));
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] CreateProgramType dto)
        {
            var programType = _programTypeService.AddNewProgramType(dto);

            if (programType == null)
                return BadRequest("ProgramType with such name already exists");

            var result = _mapper.Map<ProgramTypeDto>(programType);

            return CreatedAtAction(nameof(Get), new { id = programType.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProgramTypeDto dto)
        {
            var programType = _programTypeService.UpdateProgramType(id, dto);

            if (programType == null)
                return BadRequest("Another programType with such FitnessProgramName already exists");

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] UpdateProgramType dto)
        {
            var programType = _programTypeService.UpdateProgramTypeDetails(id, dto);

            if (programType == null)
                return NotFound();

            var result = _mapper.Map<ProgramTypeDto>(programType);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _programTypeService.RemoveProgramTypeById(id);
            return NoContent();
        }
    }
}
