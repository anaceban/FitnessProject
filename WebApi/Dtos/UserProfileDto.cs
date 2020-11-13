﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public class UserProfileDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Range(1940, 2010)]

        public int YearOfBirth { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string PrimaryGoal { get; set; }
        [Required]
        public string LevelOfFitnessExperience { get; set; }
    }
}
