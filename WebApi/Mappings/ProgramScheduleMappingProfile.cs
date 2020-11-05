using ApplicationFitness.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using AutoMapper;

namespace WebApi.Mappings
{
    public class ProgramScheduleMappingProfile : Profile
    {
        public ProgramScheduleMappingProfile()
        {
            CreateMap<ProgramSchedule, ProgramScheduleDto>();
        }
    }
}
