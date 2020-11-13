using ApplicationFitness;
using ApplicationFitness.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class UserScheduleService : IUserScheduleService
    {
        private readonly IRepository<UserSchedule> _userScheduleRepository;
        public UserScheduleService(IRepository<UserSchedule> userScheduleRepository)
        {
            _userScheduleRepository = userScheduleRepository;
        }
        public UserSchedule AddNewUserProgramSchedule(UserProgramDto dto)
        {
            UserSchedule userProgram = new UserSchedule 
            { 
                UserId = dto.UserId, 
                ProgramScheduleId = dto.ProgramScheduleId 
            };
            _userScheduleRepository.Add(userProgram);
            _userScheduleRepository.Save();
            return userProgram;
            
        }


        public List<UserSchedule> GetAllUsersWithProgram()
        {
            return _userScheduleRepository.GetAll().ToList();
        }

        public UserSchedule GetUserScheduleById(int id)
        {
            var userProgram = _userScheduleRepository.Find(id);
            if (userProgram == null)
            {
                return null;
            }
            else return userProgram;
        }
    }
}
