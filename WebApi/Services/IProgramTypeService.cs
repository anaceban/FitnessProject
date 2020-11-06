using ApplicationFitness.Domain.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace WebApi.Services
{
    public interface IProgramTypeService
    {
        IList<ProgramType> GetProgramTypes();

        ProgramType GetProgramById(int id);

        ProgramType AddNewProgramType(CreateProgramType dto);

        ProgramType UpdateProgramTypeDetails(int id, UpdateProgramType dto);

        ProgramType UpdateProgramType(int id, ProgramTypeDto dto);

        bool RemoveProgramTypeById(int id);
    }
}
