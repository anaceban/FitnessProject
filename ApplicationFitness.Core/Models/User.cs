using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationFitness.Domain.Models
{
    public class User : IdentityUser<int>
    { 
        public int Weight { get; set; }
        public int Height { get; set; }

        public string Gender { get; set; }
        public string PrimaryGoal { get; set; }
        public string LevelOfFitnessExperience { get; set; }

        public List<UserSchedule> UserSchedules { get; set; }
        public List <Review> Reviews { get; set; } 
    }

    
}
