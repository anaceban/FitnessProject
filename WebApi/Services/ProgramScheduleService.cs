using ApplicationFitness;
using ApplicationFitness.Domain.Models;
using AutoMapper;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Repositories;
using WebApi.Sorting;

namespace WebApi.Services
{
    public class ProgramScheduleService : IProgramScheduleService
    {
        private readonly IRepository<ProgramSchedule> _programScheduleRepository;
        private readonly IUserScheduleService _userScheduleService;
        private readonly IDishService _dishService;
        private readonly IMapper _mapper;
        private readonly FitnessAppContext _context;
        public ProgramScheduleService(IRepository<ProgramSchedule> repository, IUserScheduleService userScheduleService
            , IDishService dishService, IMapper mapper, FitnessAppContext context)
        {
            _programScheduleRepository = repository;
            _userScheduleService = userScheduleService;
            _dishService = dishService;
            _mapper = mapper;
            _context = context;
        }

        public ProgramSchedule AddNewProgramSchedule(CreateProgramScheduleDto dto)
        {
            var program = new ProgramSchedule
            {
                ProgramTypeId = dto.ProgramTypeId,
                FitnessProgramName = dto.FitnessProgramName,
                NutritionProgramName = dto.NutritionProgramName,
                FitnessProgramDescription = dto.FitnessProgramDescription,
                NutritionProgramDescription = dto.NutritionProgramDescription
            };

            _programScheduleRepository.Add(program);
            _programScheduleRepository.Save();
            return program;
        }

        public ProgramSchedule GetProgramScheduleById(int id)
        {
            return _programScheduleRepository.Find(id);
        }

        public IList<ProgramSchedule> GetProgramSchedules()
        {
            return _programScheduleRepository.GetAll().ToList();
        }

        public bool RemoveProgramScheduleById(int id)
        {
            var program = _programScheduleRepository.Find(id);
            if (program != null)
            {
                _programScheduleRepository.Delete(program);
                _programScheduleRepository.Save();
                return true;
            }
            else return false;
        }

        public ProgramSchedule UpdateProgramSchedule(int id, CreateProgramScheduleDto dto)
        {
            ProgramSchedule programSchedule = _programScheduleRepository.Find(id);
            if(programSchedule == null)
            {
                return null;
            }
            else if (dto.FitnessProgramName != programSchedule.FitnessProgramName)
                return null;

            programSchedule.ProgramTypeId = dto.ProgramTypeId;
            programSchedule.FitnessProgramName = dto.FitnessProgramName;
            programSchedule.NutritionProgramName = dto.NutritionProgramName;
            programSchedule.FitnessProgramDescription = dto.FitnessProgramDescription;
            programSchedule.NutritionProgramDescription = dto.NutritionProgramDescription;
            _programScheduleRepository.Save();
            return programSchedule;
        }
        public ProgramSchedule PartialUpdate(int id, UpdateProgramScheduleDto dto)
        {
            ProgramSchedule programSchedule = _programScheduleRepository.Find(id);
            if (programSchedule == null)
                return null;

            if (!string.IsNullOrWhiteSpace(dto.FitnessProgramName))
                programSchedule.FitnessProgramName = dto.FitnessProgramName;
            
            
            programSchedule.ProgramTypeId = dto.ProgramTypeId;

            if (!string.IsNullOrWhiteSpace(dto.NutritionProgramName))
                programSchedule.NutritionProgramName = dto.NutritionProgramName;

            _programScheduleRepository.Save();
            return programSchedule;
        }

        public ProgramSchedule FindProgramForUser(User user)
        {
            var schedule = _programScheduleRepository.Find(p => p.NutritionProgramName == user.PrimaryGoal && p.FitnessProgramName == user.LevelOfFitnessExperience);
            if(schedule != null)
            {
                _userScheduleService.AddNewUserProgramSchedule(user, schedule);
                return schedule;
            }
            else return null;
        }

        public ProgramSchedule GetProgramForUser(User user)
        {
            var schedule = _programScheduleRepository.Find(p => p.NutritionProgramName == user.PrimaryGoal && p.FitnessProgramName == user.LevelOfFitnessExperience);
            if (schedule != null)
                return schedule;
            else return null;
        }
        public IEnumerable<ProgramSchedule> GetProgramSchedules(SampleFilterModel filter)
        {
            var propertyInfo = typeof(ProgramSchedule);
            
            var propery = propertyInfo.GetProperty(filter.SortedField ?? "FitnessProgramName");
            if (string.IsNullOrEmpty(filter.Term))
            {
                var result = GetProgramSchedules() as IEnumerable<ProgramSchedule>;
                result = filter.SortAsc ? result.OrderBy(p => propery.GetValue(p)) : result.OrderByDescending(p => propery.GetValue(p));
                return result;
            }
            var result2 = _programScheduleRepository.GetAll().AsEnumerable().Where(u => u.FitnessProgramName.StartsWith(filter.Term) || u.NutritionProgramName.StartsWith(filter.Term));
            result2 = filter.SortAsc ? result2.OrderBy(p => propery.GetValue(p)) : result2.OrderByDescending(p => propery.GetValue(p));
            return result2;
        }

        public List<ProgramDishDto> GetDishes(User user)
        {
            
            var schedule = GetProgramForUser(user);
            if(schedule == null)
            {
                return null;
            }
            var dayIds = _context.ProgramDays.Where(s => s.ScheduleId == schedule.Id).ToList();
            var dayDishes = new List<DishDay>();
            var result = new List<ProgramDishDto>();
            foreach(var d in dayIds)
            {
                var item = new ProgramDishDto 
                { 
                    ProgramDayNumber = d.Id, 
                    Dishes = _dishService.GetDishesForDay(d.Id).Select(r => _mapper.Map<DishDto>(r)), 
                    TrainingLink = d.TrainingLink 
                };
                result.Add(item);
            }
            return result.ToList();
        }

        public ProgramScheduleDto GetProgramByTypeId(int typeId)
        {
            var schedule = _programScheduleRepository.GetAll().SingleOrDefault(t => t.ProgramTypeId == typeId);
            var result = _mapper.Map<ProgramScheduleDto>(schedule);
            return result;
        }
    }
}
