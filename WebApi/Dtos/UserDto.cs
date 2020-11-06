using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Year { get; set; }
        public string Password { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int YearOfBirth { get; set; }
        public string Gender { get; set; }
        public string PrimaryGoal { get; set; }
        public string LevelOfFitnessExperience { get; set; }
    }
}
