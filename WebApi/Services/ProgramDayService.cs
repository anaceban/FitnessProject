using ApplicationFitness;
using ApplicationFitness.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Repositories;
using WebApi.Services.Interfaces;
using WebApi.Sorting;

namespace WebApi.Services
{
    public class ProgramDayService : IProgramDayService
    {
        private readonly FitnessAppContext _context;
        private readonly IMapper _mapper;
        private readonly IRepository<ProgramDay> _repository;
        public ProgramDayService(FitnessAppContext context, IMapper mapper, IRepository<ProgramDay> repository)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;
        }

        public DishDay AddNewDishDay(DayDishDto dishDay)
        {
            var dayDish = new DishDay { DishId = dishDay.DishId, ProgramDayId = dishDay.ProgramDayId };
            _context.DishDays.Add(dayDish);
            _context.SaveChanges();
            return dayDish;
        }

        public ProgramDay AddNewProgramDay(AddDishDay dto)
        {
            var day = new ProgramDay
            {
                Name = dto.DayName,
                TrainingLink = dto.TrainingLink,
                ScheduleId = dto.ScheduleId
            };
            _repository.Add(day);

            _repository.Add(day);
            _context.SaveChanges();
            foreach (var d in dto.dishes)
            {
                _context.DishDays.Add(new DishDay
                {
                    DishId = d,
                    ProgramDayId = day.Id
                });
            }
            _context.SaveChanges();
            return day;
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

        public IEnumerable<ProgramDay> GetProgramDays()
        {
            return _repository.GetAll();
        }

        public IEnumerable<ProgramDay> GetProgramDays(SampleFilterModel filter)
        {
            var properyInfo = typeof(ProgramDay);
            var propery = properyInfo.GetProperty(filter.SortedField ?? "TrainingLink");
            if (string.IsNullOrEmpty(filter.Term))
            {
                var allDays = GetProgramDays();
                allDays = filter.SortAsc ? allDays.OrderBy(p => propery.GetValue(p)) : allDays.OrderByDescending(p => propery.GetValue(p));
                return allDays;
            }
            var days = _context.ProgramDays.Where(u => u.Name.StartsWith(filter.Term) || u.TrainingLink.StartsWith(filter.Term)).AsEnumerable();
            days = filter.SortAsc ? days.OrderBy(p => propery.GetValue(p)) : days.OrderByDescending(p => propery.GetValue(p));
            return days.ToList();
        }

        public IEnumerable<int> GetProgramDaysIds()
        {
            var daysIds = _context.ProgramDays.Select(d => d.Id);
            return daysIds;
        }
    }
}
