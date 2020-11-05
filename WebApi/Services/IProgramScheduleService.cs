using ApplicationFitness.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace WebApi.Services
{
    public interface IProgramScheduleService
    {
        IList<ProgramSchedule> GetProgramSchedules();
        ProgramSchedule GetProgramScheduleById(int id);
        bool RemoveProgramScheduleById(int id);
        ProgramSchedule AddNewProgramSchedule(CreateProgramScheduleDto dto);
        ProgramSchedule UpdateProgramSchedule(int id, CreateProgramScheduleDto dto);
        ProgramSchedule PartialUpdate(int id, UpdateProgramScheduleDto dto);

    }
}
