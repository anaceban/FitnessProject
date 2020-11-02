using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationFitness.Domain.Models
{
    public class Dish : BaseEntity
    {
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public List<DishDay> DishDays { get; set; }
    }
}
