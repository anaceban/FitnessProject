using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationFitness.Domain.Models
{
    public class Review : BaseEntity
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
    }
}
