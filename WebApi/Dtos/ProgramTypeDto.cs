﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class ProgramTypeDto
    {
        [Required]
        public string Name { get; set; }
    }
}
