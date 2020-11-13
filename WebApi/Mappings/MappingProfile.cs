using ApplicationFitness.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using AutoMapper;
using ApplicationFitness.Domain.Models.Auth;
using WebApi.Identity;

namespace WebApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProgramSchedule, ProgramScheduleDto>();
            CreateMap<ProgramType, ProgramTypeDto>();
            CreateMap<ReviewDto, Review>();
            CreateMap<User, RegisterModelDto>();
            CreateMap<User, UserProfileDto>();
            CreateMap<User, LoginModelDto>();
            CreateMap<User, ChangeUserDto>();
            CreateMap<Dish, DishDto>();
            CreateMap<UserSchedule, UserProgramDto>();
        }
    }
}
