using ApplicationFitness;
using ApplicationFitness.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class ProgramDayService : IProgramDayService
    {
        private readonly FitnessAppContext _context;
        private readonly IMapper _mapper;
        public ProgramDayService(FitnessAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<ProgramDayDto> GetProgramDayByScheduleId(int scheduleId)
        {
            var days = _context.ProgramDays.Where(s => s.ScheduleId == scheduleId);
            if (days == null)
            {
                return null;
            }
            var result = _mapper.Map<List<ProgramDayDto>>(days);
            return result;
        }

        public IEnumerable<int> GetProgramDayIdsByScheduleId(int id)
        {
            var daysIds = _context.ProgramDays.Where(s => s.ScheduleId == id).Select(d => d.Id);
            if (daysIds == null)
            {
                return null;
            }
            return daysIds;
        }
    }
}
