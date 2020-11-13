using ApplicationFitness.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class DishService : IDishService
    {
        private readonly IRepository<Dish> _dishRepository;
        public DishService(IRepository<Dish> repository)
        {
            _dishRepository = repository;
        }
        public Dish AddNewProgramDish(DishDto dto)
        {
            var dish = new Dish
            {
                Name = dto.Name,
                Quantity = dto.Quantity
                
            };
            foreach(var d in _dishRepository.GetAll())
            {
                if (d.Name.Equals(dish.Name))
                {
                    throw new Exception("Such dish already exists");
                }
            }
            _dishRepository.Add(dish);
            _dishRepository.Save();
            return dish;
        }

        public Dish GetProgramDishById(int id)
        {
            return _dishRepository.Find(id);
        }

        public List<Dish> GetProgramDishes()
        {
            return _dishRepository.GetAll().ToList();
        }

        public bool RemoveProgramDishById(int id)
        {
            var dish = _dishRepository.Find(id);
            if (dish != null)
            {
                _dishRepository.Delete(dish);
                _dishRepository.Save();
                return true;
            }
            else return false;
        }
    }
}
