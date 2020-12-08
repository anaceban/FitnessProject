using ApplicationFitness.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace WebApi.Services
{
    public interface IDishService
    {
        List<Dish> GetProgramDishes();

        Dish GetProgramDishById(int id);

        Dish AddNewProgramDish(DishDto dto);


        bool RemoveProgramDishById(int id);
        IEnumerable<Dish> GetProgramDishes(string startsWith);
        Dish UpdateProgramDish(DishDto dto, int id);

        IEnumerable<Dish> GetDishesForDay(int dayId);
    }
}
