﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class CreateProgramScheduleDto
    {
        [Required]
        public int ProgramTypeId { get; set; }
        [Required]
        public string FitnessProgramName { get; set; }
        [Required]
        public string NutritionProgramName { get; set; }
    }
}
