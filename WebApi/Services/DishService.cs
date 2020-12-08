using ApplicationFitness.Domain.Models;
using ApplicationFitness.Infrastracture.Repositories;
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
        private readonly IDishRepository _dishRepository;
        public DishService(IDishRepository repository)
        {
            _dishRepository = repository;
        }
        public Dish AddNewProgramDish(DishDto dto)
        {
            var dish = new Dish
            {
                Name = dto.Name,
                TypeOfMeal = dto.TypeOfMeal

            };

            _dishRepository.Add(dish);
            _dishRepository.Save();
            return dish;
        }

        public IEnumerable<Dish> GetDishesForDay(int dayId)
        {
            var dishes = _dishRepository.GetDishForDay(dayId);
            return dishes;
        }

        public Dish GetProgramDishById(int id)
        {
            return _dishRepository.Find(id);
        }

        public List<Dish> GetProgramDishes()
        {
            return _dishRepository.GetAll().ToList();
        }

        public IEnumerable<Dish> GetProgramDishes(string startsWith)
        {
            if (string.IsNullOrEmpty(startsWith))
                return GetProgramDishes();
            var dishes = _dishRepository.GetAll().Where(d => d.Name.StartsWith(startsWith)).OrderBy(d => d.Name);
            return dishes;
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
            return false;
        }

        public Dish UpdateProgramDish(DishDto dto, int id)
        {
            var dish = _dishRepository.Find(id);
            if(dish == null)
            {
                return null;
            }
            dish.Name = dto.Name;
            dish.TypeOfMeal = dto.TypeOfMeal;
            _dishRepository.Save();
            return dish;
        }
    }
}
