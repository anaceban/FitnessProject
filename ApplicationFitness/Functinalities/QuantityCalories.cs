using ApplicationFitness.Domain;
using ApplicationFitness.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationFitness.Functinalities
{
    public class QuantityCalories
    {
        public double GetQuantityOfCaloriesInADay(User user)
        {
            if(user.Gender == "Woman")
            {
                return ((10 * user.Weight) + (6.25 * user.Height) + (5 * (DateTime.Now.Year - user.DateOfBirth.Year)) - 161);
            }
            else return ((10 * user.Weight) + (6.25 * user.Height) + (DateTime.Now.Year - user.DateOfBirth.Year) + 5);
        }
    }
}
