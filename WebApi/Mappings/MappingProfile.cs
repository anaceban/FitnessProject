using ApplicationFitness.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using AutoMapper;

namespace WebApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProgramSchedule, ProgramScheduleDto>();
            CreateMap<ProgramType, ProgramTypeDto>();
            CreateMap<ReviewDto, Review>();
        }
    }
}
