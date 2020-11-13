using ApplicationFitness.Domain.Models;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class ProgramScheduleService : IProgramScheduleService
    {
        private readonly IRepository<ProgramSchedule> _programScheduleRepository;
        public ProgramScheduleService(IRepository<ProgramSchedule> repository)
        {
            _programScheduleRepository = repository;   
        }

        public ProgramSchedule AddNewProgramSchedule(CreateProgramScheduleDto dto)
        {
            var program = new ProgramSchedule
            {
                ProgramTypeId = dto.ProgramTypeId,
                FitnessProgramName = dto.FitnessProgramName,
                NutritionProgramName = dto.NutritionProgramName  
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
            foreach (var schedule in _programScheduleRepository.GetAll())
            {
                if (schedule.FitnessProgramName == user.LevelOfFitnessExperience && schedule.NutritionProgramName == user.PrimaryGoal)
                {
                    return schedule;
                }
                else return null;
            }
            return null;
        }
    }
}
