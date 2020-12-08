using ApplicationFitness.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Repositories;
using WebApi.Sorting;

namespace WebApi.Services
{
    public class ProgramTypeService : IProgramTypeService
    {
        private readonly IRepository<ProgramType> _typeRepository;
        public ProgramTypeService(IRepository<ProgramType> repository)
        {
            _typeRepository = repository;
        }

        public ProgramType AddNewProgramType(CreateProgramType dto)
        {
            var programType = new ProgramType
            {
                Name = dto.Name
            };
            _typeRepository.Add(programType);
            _typeRepository.Save();
            return programType;
        }

        public ProgramType GetProgramById(int id)
        {
            return _typeRepository.Find(id);
        }

        public List<ProgramType> GetProgramTypes()
        {
            return _typeRepository.GetAll().ToList();
        }

        public IEnumerable<ProgramType> GetTypesFiltered(SampleFilterModel filter)
        {
            var propertyInfo = typeof(ProgramType);
            var property = propertyInfo.GetProperty(filter.SortedField ?? "Name");
            if (string.IsNullOrEmpty(filter.Term))
            {
                var allTypes = GetProgramTypes() as IEnumerable<ProgramType>;
                allTypes = filter.SortAsc ? allTypes.OrderBy(p => property.GetValue(p)) : allTypes.OrderByDescending(p => property.GetValue(p));
                return allTypes;
            }
            var types = _typeRepository.GetAll().Where(u => u.Name.StartsWith(filter.Term)).AsEnumerable();
            types = filter.SortAsc ? types.OrderBy(p => property.GetValue(p)) : types.OrderByDescending(p => property.GetValue(p));
            return types;
        }

        public bool RemoveProgramTypeById(int id)
        {
            var type = _typeRepository.Find(id);
            if (type != null)
            {
                _typeRepository.Delete(type);
                _typeRepository.Save();
                return true;
            }
            else return false;
        }

        public ProgramType UpdateProgramType(int id,ProgramTypeDto dto)
        {
            var type = _typeRepository.Find(id);
            if(type == null)
            {
                throw new Exception("Type not found");
            }
            type.Name = dto.Name;
            _typeRepository.Save();
            return type;
        }

        public ProgramType UpdateProgramTypeDetails(int id, UpdateProgramType dto)
        {
            var type = _typeRepository.Find(id);
            if (type == null)
                return null;
            type.Name = dto.Name;
            _typeRepository.Save();
            return type;
        }

         
    }
}
