using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationFitness.Domain.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int YearOfBirth { get; set; }
        
        public int Weight { get; set; }
        public int Height { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Gender { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
        public List<UserSchedule> UserSchedules { get; set; }
        public List <Review> Reviews { get; set; } 

    }

    
}
