﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class DishDto
    {
        [Required]
        public string Name { get; set; }
        public int? Quantity { get; set; }
    }
}
