using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationFitness.Domain.Models
{
    public class Role : BaseEntity
    {
        public string Type { get; set; }
        public List<User> Users { get; set; }
    }
}
